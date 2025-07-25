using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class RecipeStorageUnitRepository : RepositoryBase<RecipeStorageUnit>, IRecipeStorageUnitRepository
{
    public RecipeStorageUnitRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }
    
    public async Task<IEnumerable<RecipeStorageUnit>> GetRecipeStorageUnitsAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .OrderBy(rsu => rsu.RecipeId)
            .ToListAsync();
    }
}
