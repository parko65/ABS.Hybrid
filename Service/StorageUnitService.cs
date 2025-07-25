using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;
internal sealed class StorageUnitService : IStorageUnitService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public StorageUnitService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StorageUnitDto>> GetStorageUnitsAsync(bool trackChanges)
    {
        var storageUnits = await _repository.StorageUnit.GetStorageUnitsAsync(trackChanges);

        var storageUnitDtos = _mapper.Map<IEnumerable<StorageUnitDto>>(storageUnits);

        return storageUnitDtos;
    }

    public async Task<IEnumerable<StorageUnitDto>> GetStorageUnitsWithMaterialAsync(bool trackChanges)
    {
        var storageUnits = await _repository.StorageUnit.GetStorageUnitsWithMaterialAsync(trackChanges);

        var storageUnitDtos = _mapper.Map<IEnumerable<StorageUnitDto>>(storageUnits);

        return storageUnitDtos;
    }

    public async Task<IEnumerable<StorageUnitDto>> GetStorageUnitsByMaterialTypeWithMaterialAsync(int materialTypeId, bool trackChanges)
    {
        var storageUnits = await _repository.StorageUnit.GetStorageUnitsByMaterialTypeWithMaterialAsync(materialTypeId, trackChanges);
        
        var storageUnitDtos = _mapper.Map<IEnumerable<StorageUnitDto>>(storageUnits);

        return storageUnitDtos;
    }

    public async Task<IEnumerable<StorageUnitDto>> GetStorageUnitsByMaterialTypeWithMaterialAsync(string materialTypeName, bool trackChanges)
    {
        var storageUnits = await _repository.StorageUnit.GetStorageUnitsByMaterialTypeWithMaterialAsync(materialTypeName, trackChanges);

        var storageUnitDtos = _mapper.Map<IEnumerable<StorageUnitDto>>(storageUnits);

        return storageUnitDtos;
    }
}
