using Entities.Models;

namespace Contracts;
public interface IRecipeStorageUnitRepository
{
    Task<IEnumerable<RecipeStorageUnit>> GetRecipeStorageUnitsAsync(bool trackChanges);
}