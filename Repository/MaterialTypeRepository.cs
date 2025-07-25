using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class MaterialTypeRepository : RepositoryBase<MaterialType>, IMaterialTypeRepository
{
    public MaterialTypeRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<MaterialType>> GetMaterialTypesAsync(bool trackChanges) =>
        await FindAll(trackChanges)
            .OrderBy(mt => mt.Name)
            .ToListAsync();

    public async Task<MaterialType?> GetMaterialTypeAsync(int id, bool trackChanges) =>
        await FindByCondition(mt => mt.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

    public async Task<MaterialType?> GetMaterialTypeByNameAsync(string name, bool trackChanges) =>
        await FindByCondition(mt => mt.Name.Equals(name.Trim()), trackChanges)
            .SingleOrDefaultAsync();
}
