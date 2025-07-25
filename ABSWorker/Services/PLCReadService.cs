using ABSWorker.Contracts;
using libplctag;
using libplctag.DataTypes;
using PLCEntities;
using Microsoft.Extensions.Options;
using ABSWorker.Configuration;

namespace ABSWorker.Services;
internal sealed class PLCReadService : IPLCReadService
{
    private readonly ILoggerManager _logger;
    private readonly PlcSettings _plcSettings;

    private const int TIMEOUT = 1000;
    private const string PATH = "1,0";
    private const Protocol PROTOCOL = Protocol.ab_eip;
    private const string PLC_ADDRESS = "140.80.0.1";
    private const string LOGGINGTAG_BASE_NAME = "N203LogBlock";

    public PLCReadService(ILoggerManager logger, IOptionsMonitor<PlcSettings> plcSettings)
    {
        _logger = logger;
        _plcSettings = plcSettings.CurrentValue;
    }

    public async Task<TagModel> ReadBasicInfoAsync(int arrayLength)
    {
        var tagModel = new TagModel();

        var result = await ReadValueAsync(LOGGINGTAG_BASE_NAME, arrayLength);
        
        if (result == null || result.Length == 0)
        {
            _logger.LogError($"Failed to read basic info from PLC at address {_plcSettings.IPAddress}");
            return tagModel;
        }

        tagModel.JobId = result[0];

        var recipeNumberUpper = result[1];
        var recipeNumberLower = result[2];

        // Combine the upper and lower 16 bits of the RecipeNumber into a single int
        tagModel.RecipeNumber = ReconstructInt(recipeNumberUpper, recipeNumberLower);

        var jobNumberUpper = result[3];
        var jobNumberLower = result[4];

        // Combine the upper and lower 16 bits of the JobNumber into a single int
        tagModel.JobNumber = ReconstructInt(jobNumberUpper, jobNumberLower);

        tagModel.BatchNumber = result[5];

        return tagModel;
    }

    /// <summary>
    /// Creates a PLC tag with the specified name and array dimensions
    /// </summary>
    private Tag<IntPlcMapper, short[]> CreateTag(string tagName, int arrayLength)
    {
        return new Tag<IntPlcMapper, short[]>
        {
            Name = tagName,
            Gateway = _plcSettings.IPAddress,
            Path = PATH,
            PlcType = PlcType.ControlLogix,
            Protocol = PROTOCOL,
            ArrayDimensions = new int[] { arrayLength },
            Timeout = TimeSpan.FromMilliseconds(TIMEOUT)
        };
    }

    /// <summary>
    /// Writes the specified values to the PLC tag and verifies the result
    /// </summary>
    private async Task<short[]?> ReadValueAsync(string tagName, int arrayLength)
    {
        try
        {
            var tag = CreateTag(tagName, arrayLength);
            var result = await tag.ReadAsync();

            if (result != null && result.Length > 0)
            {
                _logger.LogInfo($"Successfully read {result.Length} values from PLC tag '{tagName}' at address {_plcSettings.IPAddress}");
                return result;
            }
            else
            {
                _logger.LogWarn($"No data returned from PLC tag '{tagName}' at address {_plcSettings.IPAddress}");
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to read from PLC tag '{tagName}' at address {_plcSettings.IPAddress}: {ex.Message}");
            return null;
        }
    }

    public async Task<T?> ReadCustomTagAsync<T>(string tagName, int arrayLength = 1) where T : class
    {
        var result = await ReadValueAsync(tagName, arrayLength);
        if (result == null) return null;

        // Handle different return types
        if (typeof(T) == typeof(int[]))
        {
            return result.Select(s => (int)s).ToArray() as T;
        }
        if (typeof(T) == typeof(short[]))
        {
            return result as T;
        }
        if (typeof(T) == typeof(int) && result.Length > 0)
        {
            return (int)result[0] as T;
        }
        if (typeof(T) == typeof(short) && result.Length > 0)
        {
            return result[0] as T;
        }

        return null;
    }

    /// <summary>
    /// Reads a single value from the specified PLC tag
    /// </summary>
    private async Task<short?> ReadSingleValueAsync(string tagName)
    {
        var result = await ReadValueAsync(tagName, 1);
        return result?.Length > 0 ? result[0] : null;
    }

    public int[] ReconstructInts(short[] upperBits, short[] lowerBits)
    {
        if (upperBits.Length != lowerBits.Length)
            throw new ArgumentException("Arrays must have the same length");

        var result = new int[upperBits.Length];

        for (int i = 0; i < upperBits.Length; i++)
        {
            result[i] = (upperBits[i] << 16) | (lowerBits[i] & 0xFFFF);
        }

        return result;
    }

    private int ReconstructInt(short upperBits, short lowerBits)
    {
        return (upperBits << 16) | (lowerBits & 0xFFFF);
    }

    private string ReconstructString(short[] upperBits, short[] lowerBits)
    {
        if (upperBits.Length != lowerBits.Length)
            throw new ArgumentException("Arrays must have the same length");

        var chars = new char[upperBits.Length];

        for (int i = 0; i < upperBits.Length; i++)
        {
            // Combine upper 16 bits and lower 16 bits back into original int
            int combined = (upperBits[i] << 16) | (lowerBits[i] & 0xFFFF);
            chars[i] = (char)combined;
        }

        return new string(chars);
    }
}
