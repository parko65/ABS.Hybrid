using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class MaterialRepository : RepositoryBase<Material>, IMaterialRepository
{
    public MaterialRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Material>> GetMaterialsByTypeAsync(int materialTypeId, bool trackChanges) =>
        await FindByCondition(m => m.MaterialTypeId.Equals(materialTypeId), trackChanges)
            .ToListAsync();

    public async Task<Material?> GetMaterialByTypeAsync(int materialTypeId, int id, bool trackChanges) =>
        await FindByCondition(m => m.MaterialTypeId.Equals(materialTypeId) && m.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

    public async Task<Material?> GetMaterialByStorageUnitAsync(int storageUnitId, int id, bool trackChanges) =>
        await FindByCondition(m => m.StorageUnitId.Equals(storageUnitId) && m.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

    public void CreateMaterialForType(int materialTypeId, Material material)
    {
        material.MaterialTypeId = materialTypeId;

        Create(material);
    }

    public void DeleteMaterial(Material material)
    {
        var existingTrackedEntity = RepositoryContext.ChangeTracker.Entries<Material>().FirstOrDefault(e => e.Entity.Id == material.Id);

        if (existingTrackedEntity is not null)
            RepositoryContext.Entry(existingTrackedEntity.Entity).State = EntityState.Detached;

        Delete(material);
    }        
}
