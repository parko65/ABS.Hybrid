using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;
internal sealed class RecipeStorageUnitService : IRecipeStorageUnitService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public RecipeStorageUnitService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RecipeStorageUnitDto>> GetRecipeStorageUnitsAsync(bool trackChanges)
    {
        var recipeStorageUnits = await _repository.RecipeStorageUnit.GetRecipeStorageUnitsAsync(trackChanges);

        var recipeStorageUnitsDto = _mapper.Map<IEnumerable<RecipeStorageUnitDto>>(recipeStorageUnits);

        return recipeStorageUnitsDto;
    }
}
