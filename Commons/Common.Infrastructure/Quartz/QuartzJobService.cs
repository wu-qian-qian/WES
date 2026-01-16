using Common.Application.Quartz;
using Common.Domain;
using Common.Domain.Options;
using Quartz;
using Quartz.Impl.Matchers;

namespace Common.Infrastructure.Quartz;

public class QuartzJobService(ISchedulerFactory _schedulerFactory) : IQuartzJobService
{
    public async Task CraetJobAsync(JobOptions jobConfig)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        if (!typeof(BaseJob).IsAssignableFrom(jobConfig.JobType))
            throw new ArgumentException("job必须继承自BaseJob");
        var jobBuilder = JobBuilder.Create(jobConfig.JobType)
            .WithIdentity(jobConfig.JobName)
            .WithDescription(jobConfig.JobDes);

        jobBuilder.UsingJobData(ConstData.JobTimeOUtTitle, jobConfig.TimerOut);
        var jobDetail = jobBuilder.Build();

        var trigger = TriggerBuilder.Create()
            .WithSimpleSchedule(p => { p.WithInterval(new TimeSpan(0, 0, 0, 0, jobConfig.Timer)).RepeatForever(); })
            .Build();
        await scheduler.ScheduleJob(jobDetail, trigger);

        if (!jobConfig.IsStart)
            await scheduler.PauseJob(new JobKey(jobConfig.JobName));
        else
            await scheduler.ResumeJob(new JobKey(jobConfig.JobName));
    }

    public async Task StartJobAsync(string jobName)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var allJobKeys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
        if (allJobKeys.Any(p => p.Name == jobName))
            await scheduler.ResumeJob(new JobKey(jobName));
    }

    public async Task StopJobAsync(string jobName)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var jobKey = new JobKey(jobName);
        await scheduler.PauseJob(jobKey);
    }
}