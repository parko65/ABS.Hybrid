using Shared.DataTransferObjects;

namespace Service.Contracts;
public interface IRecipeStorageUnitService
{
    Task<IEnumerable<RecipeStorageUnitDto>> GetRecipeStorageUnitsAsync(bool trackChanges);
}
