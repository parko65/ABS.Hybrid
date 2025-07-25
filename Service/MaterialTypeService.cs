using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;
internal sealed class MaterialTypeService : IMaterialTypeService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public MaterialTypeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MaterialTypeDto>> GetMaterialTypesAsync(bool trackChanges)
    {
        var materialTypes = await _repository.MaterialType.GetMaterialTypesAsync(trackChanges);

        var materialTypesDto = _mapper.Map<IEnumerable<MaterialTypeDto>>(materialTypes);        
        
        return materialTypesDto;
    }

    public async Task<MaterialTypeDto> GetMaterialTypeAsync(int id, bool trackChanges)
    {
        var materialType = await _repository.MaterialType.GetMaterialTypeAsync(id, trackChanges);
        if (materialType is null)
            throw new MaterialTypeNotFoundException(id);

        var materialTypeDto = _mapper.Map<MaterialTypeDto>(materialType);
        
        return materialTypeDto;
    }

    public async Task<MaterialTypeDto> GetMaterialTypeByNameAsync(string name, bool trackChanges)
    {
        var materialType = await _repository.MaterialType.GetMaterialTypeByNameAsync(name, trackChanges);
        if (materialType is null)
            throw new MaterialTypeNotFoundException(name);

        var materialTypeDto = _mapper.Map<MaterialTypeDto>(materialType);
        
        return materialTypeDto;
    }
}
