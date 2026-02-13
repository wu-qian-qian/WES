using Common.Application.Quartz;
using Common.Domain;
using Common.Domain.Options;
using Quartz;
using Quartz.Impl.Matchers;

namespace Common.Infrastructure.Quartz;

public class QuartzJobService(ISchedulerFactory _schedulerFactory) : IQuartzJobService
{
    public static List<Type> JobTypes { get; set; } = new List<Type>();
    public void AddJobType(Type jobType)
    {
         if (!typeof(BaseJob).IsAssignableFrom(jobType))
            throw new ArgumentException("job必须继承自BaseJob");
        if (!JobTypes.Contains(jobType))
        {
            JobTypes.Add(jobType);
        }
    }

    /// <summary>
    /// CORE表达式
    /// </summary>
    /// <param name="jobConfig"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task CraetJobAsync(JobOptions jobConfig)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var JobType= JobTypes.FirstOrDefault(p => p.FullName == jobConfig.JobType);
        if (JobType == null)
        {
            throw new ArgumentException("job类型不存在");
        }
        var jobBuilder = JobBuilder.Create(JobType)
            .WithIdentity(jobConfig.JobName)
            .WithDescription(jobConfig.JobDes);

        jobBuilder.UsingJobData(ConstData.JobTimeOUtTitle, jobConfig.TimerOut);
        var jobDetail = jobBuilder.Build();

        var trigger = TriggerBuilder.Create()
        .StartAt(DateTimeOffset.Now.AddMilliseconds(3000))
        .WithIdentity(jobConfig.JobName + "Trigger")
            .WithSimpleSchedule(p => { 
                p.WithInterval(new TimeSpan(0, 0, 0, 0, jobConfig.Timer)); 
                if(jobConfig.isRepeat)
                    p.RepeatForever();
                })
            .Build();
        await scheduler.ScheduleJob(jobDetail, trigger);

        if (!jobConfig.IsStart)
            await scheduler.PauseJob(new JobKey(jobConfig.JobName));
    }

    /// <summary>
    /// 通过Cron表达式创建任务
    /// </summary>
    /// <param name="jobConfig"></param>
    /// <returns></returns>
    public async Task CraetJobAsync(JobCronOptions jobConfig)
    {
         var scheduler = await _schedulerFactory.GetScheduler();
        var JobType= JobTypes.FirstOrDefault(p => p.FullName == jobConfig.JobType);
        if (JobType == null)
        {
            throw new ArgumentException("job类型不存在");
        }
        var jobBuilder = JobBuilder.Create(JobType)
            .WithIdentity(jobConfig.JobName)
            .WithDescription(jobConfig.JobDes);

        jobBuilder.UsingJobData(ConstData.JobTimeOUtTitle, jobConfig.TimerOut);
        var jobDetail = jobBuilder.Build();

        var trigger = TriggerBuilder.Create()
            .WithCronSchedule(jobConfig.Cron)
             .StartAt(DateTimeOffset.Now.AddMilliseconds(3000))
            .WithIdentity(jobConfig.JobName + "Trigger")
            .Build();
        await scheduler.ScheduleJob(jobDetail, trigger);

        if (!jobConfig.IsStart)
            await scheduler.PauseJob(new JobKey(jobConfig.JobName));
    }
    public async Task StartJobAsync(string jobName)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var allJobKeys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
        if (allJobKeys.Any(p => p.Name == jobName))
        {
            var state = await scheduler.GetTriggerState(new TriggerKey(jobName + "Trigger"));
            if (state == TriggerState.Paused)
                 await scheduler.ResumeJob(new JobKey(jobName));
        }     
    }

    public async Task StopJobAsync(string jobName)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var allJobKeys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
        if (allJobKeys.Any(p => p.Name == jobName))
        {
            var state = await scheduler.GetTriggerState(new TriggerKey(jobName + "Trigger"));
            if (!(state == TriggerState.Paused))
                await scheduler.PauseJob(new JobKey(jobName));
        }
    }
}