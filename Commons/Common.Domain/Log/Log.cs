namespace Common.Domain.Log;

public record HttpLog(string IP, string URL, long TimeUsed, string Request, string Response);

public record EventLog(string EventName, string Request, string Message);

public record ExceptionLog(string EventName, Exception ex);

public record JobLog(string JobName, string Message);

/// <summary>
///     模块日志
/// </summary>
/// <param name="ModuleName"></param>
/// <param name="Message"></param>
public record ModuleLog(string ModuleName, string Message);