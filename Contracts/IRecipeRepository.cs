using Entities.Models;

namespace Contracts;

public interface IRecipeRepository
{
    Task<Recipe?> GetRecipeAsync(int recipeId, bool trackChanges);
    Task<IEnumerable<Recipe>> GetRecipesAsync(bool trackChanges);
    void CreateRecipe(Recipe recipe);
    Task<Recipe?> GetRecipeByNameAsync(string recipeName, bool trackChanges);
    void DeleteRecipe(Recipe recipe);
}
