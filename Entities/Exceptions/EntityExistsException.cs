namespace Entities.Exceptions;
public abstract class EntityExistsException : Exception
{
    protected EntityExistsException(string message)
        : base(message)
    {
    }    
}
