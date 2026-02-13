namespace Common.Domain.Options;

/// <summary>
///  用于JOB业务所以放到应用层
/// </summary>
/// <param name="JobType"></param>
/// <param name="JobName"></param>
/// <param name="JobDes"></param>
/// <param name="IsStart"></param>
/// <param name="Timer"></param>
/// <param name="TimerOut"></param>
public record JobOptions(Type JobType, string JobName, string JobDes, bool IsStart, int Timer, int TimerOut);