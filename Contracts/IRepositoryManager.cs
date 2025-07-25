namespace Contracts;

public interface IRepositoryManager
{
    IRecipeRepository Recipe { get; }
    IMaterialTypeRepository MaterialType { get; }
    IStorageUnitRepository StorageUnit { get; }
    IMaterialRepository Material { get; }
    IRecipeStorageUnitRepository RecipeStorageUnit { get; }
    IJobRepository Job { get; }
    IDestinationRepository Destination { get; }
    Task SaveAsync();
}
