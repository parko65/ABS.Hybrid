namespace Entities.Exceptions;
public sealed class MaterialNotFoundException : NotFoundException
{
    public MaterialNotFoundException(int id)
        : base($"Material with id: {id} doesn't exist in the database.")
    {
    }
}
