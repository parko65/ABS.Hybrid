namespace Entities.Exceptions;
public sealed class RecipeNotFoundException : NotFoundException
{
    public RecipeNotFoundException(int recipeId)
        : base($"Recipe with id: {recipeId} doesn't exist in the database.")
    {
    }
}