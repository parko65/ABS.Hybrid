using Entities.Models;

namespace Contracts;
public interface IDestinationRepository
{
    Task<IEnumerable<Destination>> GetDestinationsAsync(bool trackChanges);
    Task<Destination?> GetDestinationAsync(int destinationId, bool trackChanges);
}
