using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;
using AutoMapper;
using Entities.Exceptions;
using Entities.Models;

namespace Service;

internal sealed class RecipeService : IRecipeService
{
    private readonly IRepositoryManager _repository;    
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public RecipeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RecipeDto>> GetRecipesAsync(bool trackChanges)
    {
        var recipes = await _repository.Recipe.GetRecipesAsync(trackChanges);

        var recipeDtos = _mapper.Map<IEnumerable<RecipeDto>>(recipes);

        return recipeDtos;
    }

    public async Task<RecipeDto> GetRecipeAsync(int id, bool trackChanges)
    {
        var recipe = await _repository.Recipe.GetRecipeAsync(id, trackChanges);
        if (recipe is null)
            throw new RecipeNotFoundException(id);

        var recipeDto = _mapper.Map<RecipeDto>(recipe);

        return recipeDto;
    }

    public async Task<RecipeDto> CreateRecipeAsync(RecipeForCreationDto recipeForCreation, bool trackChanges)
    {
        var existingRecipe = await _repository.Recipe.GetRecipeByNameAsync(recipeForCreation.Name!, trackChanges);
        if (existingRecipe != null)
            throw new RecipeAlreadyExistsException(recipeForCreation.Name!);

        var recipeEntity = _mapper.Map<Recipe>(recipeForCreation);

        _repository.Recipe.CreateRecipe(recipeEntity);
        await _repository.SaveAsync();

        var recipeToReturn = _mapper.Map<RecipeDto>(recipeEntity);

        return recipeToReturn;
    }

    public async Task DeleteRecipeAsync(int recipeId, bool trackChanges)
    {
        var recipe = await _repository.Recipe.GetRecipeAsync(recipeId, trackChanges);
        if (recipe is null)
            throw new RecipeNotFoundException(recipeId);

        _repository.Recipe.DeleteRecipe(recipe);

        await _repository.SaveAsync();
    }
}