using Entities.Models;

namespace Contracts;
public interface IStorageUnitRepository
{
    Task<StorageUnit?> GetStorageUnitAsync(int storageUnitId, bool trackChanges);
    Task<IEnumerable<StorageUnit>> GetStorageUnitsAsync(bool trackChanges);
    Task<IEnumerable<StorageUnit>> GetStorageUnitsByMaterialTypeWithMaterialAsync(int materialTypeId, bool trackChanges);
    Task<IEnumerable<StorageUnit>> GetStorageUnitsByMaterialTypeWithMaterialAsync(string materialTypeName, bool trackChanges);
    Task<IEnumerable<StorageUnit>> GetStorageUnitsWithMaterialAsync(bool trackChanges);
}
