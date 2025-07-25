using Contracts;
using libplctag;
using libplctag.DataTypes;
using Service.Contracts;

namespace Service;
internal sealed class PlcWriteService : IPlcWriteService
{
    private readonly ILoggerManager _logger;

    private const int TIMEOUT = 1000;
    private const string PATH = "1,0";
    private const Protocol PROTOCOL = Protocol.ab_eip;
    private const string PLC_ADDRESS = "192.168.100.10";
    private const string CONTROLTAG_BASE_NAME = "N201ControlReq";
    private const string MIXINGTAG_BASE_NAME = "N202MixingBlock";    

    public PlcWriteService(ILoggerManager logger)
    {
        _logger = logger;
    }

    public async Task<bool> StartAsync(int value)
    {
        return await WriteValueAsync(CONTROLTAG_BASE_NAME, new short[] { (short)value }, value);
    }

    public async Task<bool> WriteBasicInfoAsync(int[] values)
    {
        return await WriteValueAsync(MIXINGTAG_BASE_NAME, Array.ConvertAll(values, v => (short)v), values);
    }

    public async Task<bool> WriteHotBinsAsync(int[] values)
    {
        return await WriteValueAsync($"{MIXINGTAG_BASE_NAME}[7]", Array.ConvertAll(values, v => (short)v), values);
    }

    public async Task<bool> WriteBitumenTanksAsync(int[] values)
    {
        return await WriteValueAsync($"{MIXINGTAG_BASE_NAME}[37]", Array.ConvertAll(values, v => (short)v), values);
    }

    public async Task<bool> WriteFillerSilosAsync(int[] values)
    {
        return await WriteValueAsync($"{MIXINGTAG_BASE_NAME}[48]", Array.ConvertAll(values, v => (short)v), values);
    }

    public async Task<bool> WriteRecipeNameAsync(string recipeName)
    {
        var values = StringToShorts(recipeName);

        return await WriteValueAsync($"{MIXINGTAG_BASE_NAME}[88]", Array.ConvertAll(values, v => (short)v), values);
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
    private async Task<bool> WriteValueAsync(string tagName, short[] values, object originalValue)
    {
        var tag = CreateTag(tagName, values.Length);
        tag.Value = values;

        await tag.WriteAsync();
        var result = await tag.ReadAsync();

        var success = AreArraysEqual(result, tag.Value);
        if (success)
        {
            _logger.LogInfo($"Successfully wrote value {originalValue} to PLC at address {PLC_ADDRESS}");
        }
        else
        {
            _logger.LogError($"Failed to write value {originalValue} to PLC at address {PLC_ADDRESS}");
        }

        return success;
    }

    private short[] StringToShorts(string input)
    {
        return input.Select(c => (short)c).ToArray();
    }

    /// <summary>
    /// Compares two arrays for equality
    /// </summary>
    private bool AreArraysEqual(short[] array1, short[] array2)
    {
        if (array1.Length != array2.Length)
            return false;

        for (var i = 0; i < array1.Length; i++)
        {
            if (array1[i] != array2[i])
                return false;
        }

        return true;
    }
}
