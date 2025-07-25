using Entities.Models;

namespace Contracts;
public interface IMaterialRepository
{
    Task<IEnumerable<Material>> GetMaterialsByTypeAsync(int materialTypeId, bool trackChanges);
    Task<Material?> GetMaterialByTypeAsync(int materialTypeId, int id, bool trackChanges);
    void CreateMaterialForType(int materialTypeId, Material material);
    void DeleteMaterial(Material material);
    Task<Material?> GetMaterialByStorageUnitAsync(int storageUnitId, int id, bool trackChanges);
}
