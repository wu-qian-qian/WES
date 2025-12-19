using Common.Application.Log;
using Common.Domain.Log;

namespace Common.Infrastructure.Log;

public class SeriLogService : ILogService
{
    public void WriteInformationLog(LogCategoryType category, string message)
    {
        Serilog.Log.ForContext("Category", category)
            .Information("{Info}"
                , message);
    }

    public void WriteErrorLog(LogCategoryType category, string message)
    {
        Serilog.Log.ForContext("Category", category)
            .Error("{Info}"
                , message);
    }

    public void WriteLog(HttpLog log)
    {
        var request = log.Request
            .Replace("\r", "")
            .Replace("\n", "");

        var response = log.Response
            .Replace("\r", "")
            .Replace("\n", "");
        Serilog.Log.ForContext("Category", LogCategoryType.Http).Information(
            "URL: {URL}, TimeUsed: {TimeUsed}, Request: {Request}, Response: {Response},IP: {IP}"
            , log.URL, log.TimeUsed, request, response, log.IP);
    }

    public void WriteLog(EventLog log)
    {
        Serilog.Log.ForContext("Category", LogCategoryType.Event).Information(
            "{EventName},{Request},{Info}"
            , log.EventName, log.Request, log.Message);
    }


    public void WriteLog(ExceptionLog log)
    {
        Serilog.Log.ForContext("Category", LogCategoryType.Exception)
            .Information(log.ex, log.EventName);
    }

    public void WriteLog(JobLog log)
    {
        Serilog.Log.ForContext("Category", LogCategoryType.Job).Information(
            "{ThreadId},{JobName},{Info}"
            , Thread.CurrentThread.ManagedThreadId, log.JobName, log.Message);
    }


    public async Task UpLoadFileAsync()
    {
        throw new NotImplementedException();
    }
}