using Contracts;
using libplctag;
using libplctag.DataTypes;
using Service.Contracts;

namespace Service;
internal sealed class PlcReadService : IPlcReadService
{
    private readonly ILoggerManager _logger;

    private const int TIMEOUT = 1000;
    private const string PATH = "1,0";
    private const Protocol PROTOCOL = Protocol.ab_eip;
    private const string PLC_ADDRESS = "192.168.100.10";
    private const string CONTROLSTATUSTAG_BASE_NAME = "N200AlarmBlock";
    private const string LOGGINGTAG_BASE_NAME = "N203LogBlock";

    public PlcReadService(ILoggerManager logger)
    {
        _logger = logger;
    }

    public async Task<int> CheckIfMixerReadyAsync()
    {
        var result = await ReadSingleValueAsync($"{CONTROLSTATUSTAG_BASE_NAME}[0]");

        return result ?? -1; // Return -1 if read fails
    }

    public async Task<int[]?> ReadBasicInfoAsync(int arrayLength)
    {
        var result = await ReadValueAsync(LOGGINGTAG_BASE_NAME, arrayLength);
        return result?.Select(s => (int)s).ToArray();
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
    /// Creates a PLC tag with the specified name and array dimensions
    /// </summary>
    private Tag<IntPlcMapper, short[]> CreateTag(string tagName, int arrayLength)
    {
        return new Tag<IntPlcMapper, short[]>
        {
            Name = tagName,
            Gateway = PLC_ADDRESS,
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
                _logger.LogInfo($"Successfully read {result.Length} values from PLC tag '{tagName}' at address {PLC_ADDRESS}");
                return result;
            }
            else
            {
                _logger.LogWarn($"No data returned from PLC tag '{tagName}' at address {PLC_ADDRESS}");
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to read from PLC tag '{tagName}' at address {PLC_ADDRESS}: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Reads a single value from the specified PLC tag
    /// </summary>
    private async Task<short?> ReadSingleValueAsync(string tagName)
    {
        var result = await ReadValueAsync(tagName, 1);
        return result?.Length > 0 ? result[0] : null;
    }
}
