using Shared.DataTransferObjects;

namespace Service.Contracts;
public interface IStorageUnitService
{
    Task<IEnumerable<StorageUnitDto>> GetStorageUnitsAsync(bool trackChanges);
    Task<IEnumerable<StorageUnitDto>> GetStorageUnitsByMaterialTypeWithMaterialAsync(int materialTypeId, bool trackChanges);
    Task<IEnumerable<StorageUnitDto>> GetStorageUnitsByMaterialTypeWithMaterialAsync(string materialTypeName, bool trackChanges);
    Task<IEnumerable<StorageUnitDto>> GetStorageUnitsWithMaterialAsync(bool trackChanges);
}
