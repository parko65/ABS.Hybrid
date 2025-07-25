using Contracts;
using Entities.Models;
using Enums;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class JobRepository : RepositoryBase<Job>, IJobRepository
{
    public JobRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    // You can add methods specific to JobRepository here if needed.

    public async Task<Job?> GetJobAsync(int jobId, bool trackChanges)
    {
        return await FindByCondition(j => j.Id.Equals(jobId), trackChanges)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Job>> GetJobsAsync(DateTime date, JobStatus status, bool trackChanges)
    {
        return await FindByCondition(j => j.CreateDate.Date == date.Date && j.Status == status, trackChanges)
            .Include(j => j.Recipe!) // Include related Recipe entity
            .ThenInclude(r => r.RecipeStorageUnits!)
            .ThenInclude(rs => rs.StorageUnit) // Include related StorageUnit entity
            .Include(j => j.Destination) // Include related Destination entity
            .OrderBy(j => j.CreateDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Job>> GetJobsAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .OrderBy(j => j.CreateDate)
            .ToListAsync();
    }

    public void CreateJobForRecipe(int recipeId, Job job)
    {
        job.Status = JobStatus.Created;
        job.RecipeId = recipeId; // Set the RecipeId for the job
        job.CreateDate = DateTime.Now; // Set the CreateDate to the current time
        Create(job);
    }

    public void DeleteJob(Job job)
    {
        var existingTrackedEntity = RepositoryContext.ChangeTracker.Entries<Job>().FirstOrDefault(e => e.Entity.Id == job.Id);

        if (existingTrackedEntity is not null)
            RepositoryContext.Entry(existingTrackedEntity.Entity).State = EntityState.Detached;

        Delete(job);
    }
}