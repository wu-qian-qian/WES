using Common.Domain.Log;

namespace Common.Application.Log;

public interface ILogService
{
    public void WriteInformationLog(LogCategoryType category, string message);

    public void WriteErrorLog(LogCategoryType category, string message);


    void WriteLog(HttpLog log);

    void WriteLog(EventLog log);

    void WriteLog(ExceptionLog log);

    void WriteLog(JobLog log);

    /// <summary>
    ///     日志上传接口
    /// </summary>
    /// <returns></returns>
    public Task UpLoadFileAsync();
}