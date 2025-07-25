using Enums;
using Shared.DataTransferObjects;

namespace Service.Contracts;
public interface IJobService
{
    Task<IEnumerable<JobDto>> GetJobsAsync(bool trackChanges);
    Task<JobDto> GetJobAsync(int jobId, bool trackChanges);
    Task<JobDto> CreateJobForRecipeAsync(int recipeId, JobForCreationDto jobForCreation, bool trackChanges);
    Task<IEnumerable<JobDto>> GetJobsAsync(DateTime date, JobStatus status, bool trackChanges);
    Task DeleteJobAsync(int jobId, bool trackChanges);
}
