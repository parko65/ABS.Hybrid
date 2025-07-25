
namespace Service.Contracts;

public interface IPlcWriteService
{
    Task<bool> StartAsync(int value);
    Task<bool> WriteBasicInfoAsync(int[] values);
    Task<bool> WriteBitumenTanksAsync(int[] values);
    Task<bool> WriteFillerSilosAsync(int[] values);
    Task<bool> WriteHotBinsAsync(int[] values);
    Task<bool> WriteRecipeNameAsync(string recipeName);
}
