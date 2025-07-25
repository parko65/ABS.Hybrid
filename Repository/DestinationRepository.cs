using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class DestinationRepository : RepositoryBase<Destination>, IDestinationRepository
{
    public DestinationRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    // You can add methods specific to DestinationRepository here if needed.

    public async Task<IEnumerable<Destination>> GetDestinationsAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .OrderBy(d => d.Name)
            .ToListAsync();
    }

    public async Task<Destination?> GetDestinationAsync(int destinationId, bool trackChanges)
    {
        return await FindByCondition(d => d.Id.Equals(destinationId), trackChanges)
            .SingleOrDefaultAsync();
    }
}
