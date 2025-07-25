namespace Entities.Exceptions;
public sealed class JobNotFoundException : NotFoundException
{
    public JobNotFoundException(int jobId)
        : base($"Job with id: {jobId} doesn't exist in the database.")
    {
    }
}