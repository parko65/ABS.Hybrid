using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;
internal sealed class MaterialService : IMaterialService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public MaterialService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    // Implement methods for IMaterialService here
    public async Task<IEnumerable<MaterialDto>> GetMaterialsByTypeAsync(int materialTypeId, bool trackChanges)
    {
        var materialType = await _repository.MaterialType.GetMaterialTypeAsync(materialTypeId, trackChanges);
        if (materialType is null)
            throw new MaterialTypeNotFoundException(materialTypeId);

        var materials = await _repository.Material.GetMaterialsByTypeAsync(materialTypeId, trackChanges);

        var materialsDto = _mapper.Map<IEnumerable<MaterialDto>>(materials);

        return materialsDto;
    }

    public async Task<MaterialDto> GetMaterialByTypeAsync(int materialTypeId, int id, bool trackChanges)
    {
        var materialType = await _repository.MaterialType.GetMaterialTypeAsync(materialTypeId, trackChanges);
        if (materialType is null)
            throw new MaterialTypeNotFoundException(materialTypeId);

        var material = await _repository.Material.GetMaterialByTypeAsync(materialTypeId, id, trackChanges);

        var materialDto = _mapper.Map<MaterialDto>(material);

        return materialDto;
    }

    public async Task<MaterialDto> CreateMaterialForTypeAsync(int materialTypeId, MaterialForCreationDto materialForCreation,
        bool trackChanges)
    {
        var materialType = await _repository.MaterialType.GetMaterialTypeAsync(materialTypeId, trackChanges);
        if (materialType is null)
            throw new MaterialTypeNotFoundException(materialTypeId);

        var materialEntity = _mapper.Map<Material>(materialForCreation);

        _repository.Material.CreateMaterialForType(materialTypeId, materialEntity);

        await _repository.SaveAsync();

        var materialDto = _mapper.Map<MaterialDto>(materialEntity);

        return materialDto;
    }

    public async Task UpdateMaterialForMaterialTypeAsync(int materialTypeId, int id, MaterialForUpdateDto materialForUpdate,
        bool materialTypeTrackChanges, bool materialTrackChanges)
    {
        var materialType = await _repository.MaterialType.GetMaterialTypeAsync(materialTypeId, materialTypeTrackChanges);
        if (materialType is null)
            throw new MaterialTypeNotFoundException(materialTypeId);

        var materialEntity = await _repository.Material.GetMaterialByTypeAsync(materialTypeId, id, materialTrackChanges);

        _mapper.Map(materialForUpdate, materialEntity);

        await _repository.SaveAsync();
    }

    public async Task UpdateMaterialForStorageUnitAsync(int storageUnitId, int materialTypeId, int id, MaterialForUpdateDto materialForUpdate,
        bool storageUnitTrackChanges, bool materialTrackChanges)
    {
        var storageUnit = await _repository.StorageUnit.GetStorageUnitAsync(storageUnitId, storageUnitTrackChanges);
        if (storageUnit is null)
            throw new StorageUnitNotFoundException(storageUnitId);

        var materialEntity = await _repository.Material.GetMaterialByTypeAsync(materialTypeId, id, materialTrackChanges);

        _mapper.Map(materialForUpdate, materialEntity);

        materialEntity.StorageUnitId = storageUnitId;

        await _repository.SaveAsync();
    }

    public async Task DeleteMaterialForMaterialTypeAsync(int materialTypeId, int id, bool trackChanges)
    {
        var materialType = await _repository.MaterialType.GetMaterialTypeAsync(materialTypeId, trackChanges);
        if (materialType is null)
            throw new MaterialTypeNotFoundException(materialTypeId);
        
        var material = await _repository.Material.GetMaterialByTypeAsync(materialTypeId, id, trackChanges);
        if (material is null)
            throw new MaterialNotFoundException(id);

        _repository.Material.DeleteMaterial(material);

        await _repository.SaveAsync();
    }
}