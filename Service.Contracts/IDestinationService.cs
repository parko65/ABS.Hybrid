using Shared.DataTransferObjects;

namespace Service.Contracts;
public interface IDestinationService
{
    Task<IEnumerable<DestinationDto>> GetDestinationsAsync(bool trackChanges);
    Task<DestinationDto> GetDestinationAsync(int destinationId, bool trackChanges);
}
