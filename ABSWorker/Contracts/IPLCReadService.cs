using PLCEntities;

namespace ABSWorker.Contracts;
public interface IPLCReadService
{
    Task<TagModel> ReadBasicInfoAsync(int arrayLength = 7);
    Task<T?> ReadCustomTagAsync<T>(string tagName, int arrayLength = 1) where T : class;
}
