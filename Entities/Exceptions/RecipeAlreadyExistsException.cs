namespace Entities.Exceptions;
public sealed class RecipeAlreadyExistsException : EntityExistsException
{
    public RecipeAlreadyExistsException(string recipeName)
        : base($"Recipe with id: {recipeName} already exists in the database.")
    {
    }
}