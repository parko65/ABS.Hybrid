namespace Shared.DataTransferObjects;
public record RecipeStorageUnitDto(int RecipeId, int StorageUnitId, double Take, StorageUnitDto? StorageUnit);