using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class StorageUnitRepository : RepositoryBase<StorageUnit>, IStorageUnitRepository
{
    public StorageUnitRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<StorageUnit>> GetStorageUnitsAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<StorageUnit>> GetStorageUnitsWithMaterialAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .Include(s => s.Material)
            .Include(s => s.MaterialType)
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<StorageUnit>> GetStorageUnitsByMaterialTypeWithMaterialAsync(int materialTypeId, bool trackChanges)
    {
        var items = await FindByCondition(s => s.MaterialTypeId.Equals(materialTypeId), trackChanges)
            .Include(s => s.Material)
            .OrderBy(s => s.Name)
            .ToListAsync();

        return items;
    }

    public async Task<IEnumerable<StorageUnit>> GetStorageUnitsByMaterialTypeWithMaterialAsync(string materialTypeName, bool trackChanges)
    {
        var items = await FindByCondition(s => s.Name!.Equals(materialTypeName), trackChanges)
            .Include(s => s.Material)
            .Include(s => s.MaterialType)
            .OrderBy(s => s.Name)
            .ToListAsync();

        return items;
    }

    public async Task<StorageUnit?> GetStorageUnitAsync(int storageUnitId, bool trackChanges)
    {
        return await FindByCondition(s => s.Id.Equals(storageUnitId), trackChanges)
            .SingleOrDefaultAsync();
    }
}