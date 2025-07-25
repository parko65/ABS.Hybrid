using Contracts;

namespace Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IRecipeRepository> _recipeRepository;
    private readonly Lazy<IMaterialTypeRepository> _materialTypeRepository;
    private readonly Lazy<IStorageUnitRepository> _storageUnitRepository;
    private readonly Lazy<IMaterialRepository> _materialRepository;
    private readonly Lazy<IRecipeStorageUnitRepository> _recipeStorageUnitRepository;
    private readonly Lazy<IJobRepository> _jobRepository;
    private readonly Lazy<IDestinationRepository> _destinationRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _recipeRepository = new Lazy<IRecipeRepository>(() => new RecipeRepository(_repositoryContext));
        _materialTypeRepository = new Lazy<IMaterialTypeRepository>(() => new MaterialTypeRepository(_repositoryContext));
        _storageUnitRepository = new Lazy<IStorageUnitRepository>(() => new StorageUnitRepository(_repositoryContext));
        _materialRepository = new Lazy<IMaterialRepository>(() => new MaterialRepository(_repositoryContext));
        _recipeStorageUnitRepository = new Lazy<IRecipeStorageUnitRepository>(() => new RecipeStorageUnitRepository(_repositoryContext));
        _jobRepository = new Lazy<IJobRepository>(() => new JobRepository(_repositoryContext));
        _destinationRepository = new Lazy<IDestinationRepository>(() => new DestinationRepository(_repositoryContext));
    }
    public IRecipeRepository Recipe => _recipeRepository.Value;
    public IMaterialTypeRepository MaterialType => _materialTypeRepository.Value;
    public IStorageUnitRepository StorageUnit => _storageUnitRepository.Value;
    public IMaterialRepository Material => _materialRepository.Value;
    public IRecipeStorageUnitRepository RecipeStorageUnit => _recipeStorageUnitRepository.Value;
    public IJobRepository Job => _jobRepository.Value;
    public IDestinationRepository Destination => _destinationRepository.Value;

    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
}
