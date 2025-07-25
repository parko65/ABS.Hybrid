namespace Entities.Exceptions;
public sealed class DestinationNotFoundException : NotFoundException
{
    public DestinationNotFoundException(int destinationId)
        : base($"Destination with id: {destinationId} doesn't exist in the database.")
    {
    }
}