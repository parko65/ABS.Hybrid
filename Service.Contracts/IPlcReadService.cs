
namespace Service.Contracts;

public interface IPlcReadService
{
    Task<int> CheckIfMixerReadyAsync();
    Task<int[]?> ReadBasicInfoAsync(int arrayLength = 7);
    Task<T?> ReadCustomTagAsync<T>(string tagName, int arrayLength = 1) where T : class;
}
