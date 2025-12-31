namespace Common.Domain.Options;

public record JobOptions(Type JobType, string JobName, string JobDes, bool IsStart, int Timer, int TimerOut);