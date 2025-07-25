using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class RecipeRepository : RepositoryBase<Recipe>, IRecipeRepository
{
    public RecipeRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Recipe>> GetRecipesAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)            
            .OrderBy(r => r.Id)
            .ToListAsync();
    }    

    public async Task<Recipe?> GetRecipeAsync(int recipeId, bool trackChanges)
    {
        return await FindByCondition(r => r.Id == recipeId, trackChanges)            
            .SingleOrDefaultAsync();
    }

    public async Task<Recipe?> GetRecipeByNameAsync(string recipeName, bool trackChanges)
    {
        return await FindByCondition(r => r.Name.Equals(recipeName), trackChanges)
            .SingleOrDefaultAsync();
    }

    public void CreateRecipe(Recipe recipe)
    {
        // ...Defaults
        recipe.VersionNumber = 1;
        recipe.CreatedDate = DateTime.Now;
        // ...

        Create(recipe);
    }

    public void DeleteRecipe(Recipe recipe)
    {
        var existingTrackedEntity = RepositoryContext.ChangeTracker.Entries<Recipe>().FirstOrDefault(e => e.Entity.Id == recipe.Id);

        if (existingTrackedEntity is not null)
            RepositoryContext.Entry(existingTrackedEntity.Entity).State = EntityState.Detached;

        Delete(recipe);
    }
}
