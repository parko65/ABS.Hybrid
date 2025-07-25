namespace Entities.Exceptions;
public sealed class StorageUnitNotFoundException : NotFoundException
{
    public StorageUnitNotFoundException(int id)
        : base($"Storage unit with id: {id} doesn't exist in the database.")
    {
    }
}
