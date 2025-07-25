using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IRecipeService
{
    Task<RecipeDto> GetRecipeAsync(int recipeId, bool trackChanges);
    Task<IEnumerable<RecipeDto>> GetRecipesAsync(bool trackChanges);
    Task<RecipeDto> CreateRecipeAsync(RecipeForCreationDto recipeForCreation, bool trackChanges);
    Task DeleteRecipeAsync(int recipeId, bool trackChanges);
}
