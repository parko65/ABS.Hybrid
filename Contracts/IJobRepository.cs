using Entities.Models;
using Enums;

namespace Contracts;
public interface IJobRepository
{
    Task<IEnumerable<Job>> GetJobsAsync(bool trackChanges);
    Task<Job?> GetJobAsync(int jobId, bool trackChanges);
    void CreateJobForRecipe(int recipeId, Job job);
    Task<IEnumerable<Job>> GetJobsAsync(DateTime date, JobStatus status, bool trackChanges);
    void DeleteJob(Job job);
}
