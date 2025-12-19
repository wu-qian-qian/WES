using Common.Domain;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Common.Infrastructure.Quartz;

public static class QuatrzJobConfiguration
{
    public static IServiceCollection AddQuatrzJobConfiguration(this IServiceCollection serviceCollection,
        int maxConcurrency = 10)
    {
        serviceCollection.AddSingleton<QuartzJobListener>();
        serviceCollection.AddQuartz(configurator =>
        {
            configurator.SchedulerId = $"{ConstData.JobScheduler.GetHashCode()}";
            configurator.SchedulerName = $"{ConstData.JobScheduler}";
            configurator.MisfireThreshold = TimeSpan.FromSeconds(8);
            configurator.InterruptJobsOnShutdownWithWait = true;
            configurator.UseDefaultThreadPool(tp =>
            {
                tp.MaxConcurrency = maxConcurrency; // 设置最大并发数
            });
            configurator.AddSchedulerListener<QuartzJobListener>();
        });
        serviceCollection.AddQuartzHostedService(configure =>
        {
            configure.AwaitApplicationStarted = true;
            configure.WaitForJobsToComplete = true;
        });

        return serviceCollection;
    }
}