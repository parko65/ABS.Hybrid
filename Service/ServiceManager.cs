using Contracts;
using Service.Contracts;
using AutoMapper;

namespace Service;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IRecipeService> _recipeService;
    private readonly Lazy<IMaterialTypeService> _materialTypeService;
    private readonly Lazy<IStorageUnitService> _storageUnitService;
    private readonly Lazy<IMaterialService> _materialService;
    private readonly Lazy<IRecipeStorageUnitService> _recipeStorageUnitService;
    private readonly Lazy<IJobService> _jobService;
    private readonly Lazy<IDestinationService> _destinationService;
    private readonly Lazy<IPlcWriteService> _plcWriteService;
    private readonly Lazy<IPlcReadService> _plcReadService;

    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
    {
        _recipeService = new Lazy<IRecipeService>(() => new RecipeService(repositoryManager, logger, mapper));
        _materialTypeService = new Lazy<IMaterialTypeService>(() => new MaterialTypeService(repositoryManager, logger, mapper));
        _storageUnitService = new Lazy<IStorageUnitService>(() => new StorageUnitService(repositoryManager, logger, mapper));
        _materialService = new Lazy<IMaterialService>(() => new MaterialService(repositoryManager, logger, mapper));
        _recipeStorageUnitService = new Lazy<IRecipeStorageUnitService>(() => new RecipeStorageUnitService(repositoryManager, logger, mapper));
        _jobService = new Lazy<IJobService>(() => new JobService(repositoryManager, logger, mapper));
        _destinationService = new Lazy<IDestinationService>(() => new DestinationService(repositoryManager, logger, mapper));
        _plcWriteService = new Lazy<IPlcWriteService>(() => new PlcWriteService(logger));
        _plcReadService = new Lazy<IPlcReadService>(() => new PlcReadService(logger));
    }

    public IRecipeService RecipeService => _recipeService.Value;
    public IMaterialTypeService MaterialTypeService => _materialTypeService.Value;
    public IStorageUnitService StorageUnitService => _storageUnitService.Value;
    public IMaterialService MaterialService => _materialService.Value;
    public IRecipeStorageUnitService RecipeStorageUnitService => _recipeStorageUnitService.Value;
    public IJobService JobService => _jobService.Value;
    public IDestinationService DestinationService => _destinationService.Value;
    public IPlcWriteService PlcWriteService => _plcWriteService.Value;
    public IPlcReadService PlcReadService => _plcReadService.Value;
}
