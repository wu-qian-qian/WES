using Common.Domain.Options;

namespace Common.Application.Quartz;

public interface IQuartzJobService
{
    public void AddJobType(Type jobType);
    
    public Task CraetJobAsync(JobOptions jobConfig);

    public  Task CraetJobAsync(JobCronOptions jobConfig);
    public Task StartJobAsync(string jobName);

    public Task StopJobAsync(string jobName);

    public Task DeleteJobAsync(string jobName);
}