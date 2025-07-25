using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;
internal sealed class DestinationService : IDestinationService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public DestinationService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DestinationDto>> GetDestinationsAsync(bool trackChanges)
    {
        var destinations = await _repository.Destination.GetDestinationsAsync(trackChanges);

        var destinationDtos = _mapper.Map<IEnumerable<DestinationDto>>(destinations);

        return destinationDtos;
    }

    public async Task<DestinationDto> GetDestinationAsync(int destinationId, bool trackChanges)
    {
        var destination = await _repository.Destination.GetDestinationAsync(destinationId, trackChanges);
        if (destination is null)
            throw new DestinationNotFoundException(destinationId);

        var destinationDto = _mapper.Map<DestinationDto>(destination);
        return destinationDto;
    }
}