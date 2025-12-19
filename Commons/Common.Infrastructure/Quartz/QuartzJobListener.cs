using Common.Application.Log;
using Quartz;
using Quartz.Listener;

namespace Common.Infrastructure.Quartz;

public class QuartzJobListener(ILogService _logService) : SchedulerListenerSupport
{
    public override Task SchedulerError(string msg, SchedulerException cause,
        CancellationToken cancellationToken = default)
    {
        return base.SchedulerError(msg, cause, cancellationToken);
    }
}