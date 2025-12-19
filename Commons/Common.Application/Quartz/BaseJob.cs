using Common.Domain;
using Quartz;

namespace Common.Application.Quartz;

public abstract class BaseJob : IJob
{
    protected CancellationTokenSource _linkedCts;

    protected void InitializaCanncellTokenSource(IJobExecutionContext context)
    {
        // 创建链接取消令牌源
        _linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
            context.CancellationToken);

        // 设置自定义超时
        var timeout = context.JobDetail.JobDataMap.GetInt(ConstData.JobTimeOUtTitle);
        _linkedCts.CancelAfter(TimeSpan.FromSeconds(timeout));
    }

    public abstract Task Execute(IJobExecutionContext context);
}