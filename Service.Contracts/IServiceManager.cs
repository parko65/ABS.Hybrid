namespace Service.Contracts;

public interface IServiceManager
{
    IRecipeService RecipeService { get; }
    IMaterialTypeService MaterialTypeService { get; }
    IStorageUnitService StorageUnitService { get; }
    IMaterialService MaterialService { get; }
    IRecipeStorageUnitService RecipeStorageUnitService { get; }
    IJobService JobService { get; }
    IDestinationService DestinationService { get; }
    IPlcWriteService PlcWriteService { get; }
    IPlcReadService PlcReadService { get; }
}
