using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Enums;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;
internal sealed class JobService : IJobService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public JobService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<JobDto>> GetJobsAsync(bool trackChanges)
    {
        var jobs = await _repository.Job.GetJobsAsync(trackChanges);
        
        var jobDtos = _mapper.Map<IEnumerable<JobDto>>(jobs);
        
        return jobDtos;
    }

    public async Task<IEnumerable<JobDto>> GetJobsAsync(DateTime date, JobStatus status, bool trackChanges)
    {
        var jobs = await _repository.Job.GetJobsAsync(date, status, trackChanges);
        
        var jobDtos = _mapper.Map<IEnumerable<JobDto>>(jobs);
        
        return jobDtos;
    }

    public async Task<JobDto> GetJobAsync(int jobId, bool trackChanges)
    {
        var job = await _repository.Job.GetJobAsync(jobId, trackChanges);
        if (job is null)
            throw new JobNotFoundException(jobId);

        var jobDto = _mapper.Map<JobDto>(job);

        return jobDto;
    }

    public async Task<JobDto> CreateJobForRecipeAsync(int recipeId, JobForCreationDto jobForCreation, bool trackChanges)
    {
        var recipe = await _repository.Recipe.GetRecipeAsync(recipeId, trackChanges);
        if (recipe is null)
            throw new RecipeNotFoundException(recipeId);

        var jobEntity = _mapper.Map<Job>(jobForCreation);

        _repository.Job.CreateJobForRecipe(recipeId, jobEntity);
        await _repository.SaveAsync();

        var jobToreturn = _mapper.Map<JobDto>(jobEntity);

        return jobToreturn;
    }

    public async Task DeleteJobAsync(int jobId, bool trackChanges)
    {
        var job = await _repository.Job.GetJobAsync(jobId, trackChanges);
        if (job is null)
            throw new JobNotFoundException(jobId);

        _repository.Job.DeleteJob(job);

        await _repository.SaveAsync();
    }
}
