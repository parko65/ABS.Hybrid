using Shared.DataTransferObjects;

namespace Service.Contracts;
public interface IMaterialService
{
    Task<IEnumerable<MaterialDto>> GetMaterialsByTypeAsync(int materialTypeId, bool trackChanges);
    Task<MaterialDto> GetMaterialByTypeAsync(int materialTypeId, int id, bool trackChanges);
    Task<MaterialDto> CreateMaterialForTypeAsync(int materialTypeId, MaterialForCreationDto materialForCreation,
        bool trackChanges);
    Task UpdateMaterialForMaterialTypeAsync(int materialTypeId, int id, MaterialForUpdateDto materialForUpdate,
        bool materialTypeTrackChanges, bool materialTrackChanges);

    Task UpdateMaterialForStorageUnitAsync(int storageUnitId, int materialtypeId, int id, MaterialForUpdateDto materialForUpdate,
        bool storageUnitTrackChanges, bool materialTrackChanges);

    Task DeleteMaterialForMaterialTypeAsync(int materialTypeId, int id, bool trackChanges);
}
