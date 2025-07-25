namespace Entities.Models;
public class RecipeStorageUnit
{
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
    public int StorageUnitId { get; set; }
    public StorageUnit StorageUnit { get; set; } = null!;
    public double Take { get; set; } // Amount of the ingredient in the storage unit
}
