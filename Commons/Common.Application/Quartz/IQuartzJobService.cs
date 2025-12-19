using Common.Domain;

namespace Common.Application.Quartz;

public interface IQuartzJobService
{
    public Task CraetJobAsync(JobOptions jobConfig);

    public Task StartJobAsync(string jobName);

    public Task StopJobAsync(string jobName);
}