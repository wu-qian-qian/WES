using Common.Application.Log;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Common.Infrastructure.Log;

public static class SerilogLogConfiguration
{
    /// <summary>
    /// 自定义规则配置
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="logConfiguration"></param>
    /// <param name="logConfigActions"></param>
    /// <returns></returns>
    public static IServiceCollection AddSeriLogConfiguration(this IServiceCollection serviceCollection,
        LoggerConfiguration logConfiguration,
        params Action<LoggerConfiguration>[] logConfigActions)
    {
        // var loggerConfiguration=new LoggerConfiguration()
        //     .MinimumLevel.Information()
        //     .Enrich.FromLogContext();
        foreach (var configAction in logConfigActions) logConfiguration.WriteTo.Logger(configAction);
        serviceCollection.AddSingleton<ILogService, SeriLogService>();
        return serviceCollection;
    }

    private static void SerilogConfiguration()
    {
        //配置日志
        Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            // 系统日志
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Properties.ContainsKey("Category") == false)
                .WriteTo.File("Logs/systems/system-.log", rollingInterval: RollingInterval.Day))
            // 业务日志
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Properties.ContainsKey("Category") &&
                                             e.Properties["Category"].ToString() == "Business")
                .WriteTo.File("Logs/business/business-.log", rollingInterval: RollingInterval.Day))
            //Http日志 配置使用了json格式模板
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Properties.ContainsKey("Category") &&
                                             e.Properties["Category"].ToString() == "Http")
                .WriteTo.File("Logs/https/http-.log", rollingInterval: RollingInterval.Day,
                    outputTemplate:
                    @"{{{NewLine}""Timestamp"": ""{Timestamp:o}"",{NewLine}""Level"": ""{Level}"",{NewLine}""IP"": ""{IP}"",{NewLine}""URL"": ""{URL}"",{NewLine}""Request"": ""{Request}"",{NewLine}""Response"": ""{Response}"",{NewLine}""TimeUsed"": ""{TimeUsed}"",{NewLine}""Info"": ""{Info}""{NewLine}}{NewLine}{NewLine}"))
            //error日志
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Properties.ContainsKey("Category") &&
                                             e.Properties["Category"].ToString() == "Exception")
                .WriteTo.File("Logs/exceptions/exception-.log", rollingInterval: RollingInterval.Day))
            //Event日志
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Properties.ContainsKey("Category") &&
                                             e.Properties["Event"].ToString() == "Event")
                .WriteTo.File("Logs/events/event-.log", rollingInterval: RollingInterval.Day,
                    outputTemplate:
                    @"{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] [Event:{EventName}] [Parameter:{Request}] [Info:{Info}]{NewLine}"))
            //后台任务
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Properties.ContainsKey("Category") &&
                                             e.Properties["Category"].ToString() == "Job")
                .WriteTo.File("Logs/jobs/job-.log", rollingInterval: RollingInterval.Day,
                    outputTemplate:
                    @"{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] [TID:{ThreadId}] [Job:{JobName}] [Info:{Info}] {NewLine}"))
            //其他
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Properties.ContainsKey("Category") &&
                                             e.Properties["Category"].ToString() == "Other")
                .WriteTo.File("Logs/others/other-.log", rollingInterval: RollingInterval.Day))
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Properties.ContainsKey("Category") &&
                                             e.Properties["Category"].ToString() == "Communication")
                .WriteTo.File("Logs/others/other-.log", rollingInterval: RollingInterval.Day))
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Properties.ContainsKey("Category") &&
                                             e.Properties["Category"].ToString() == "Module")
                .WriteTo.File("Logs/others/other-.log", rollingInterval: RollingInterval.Day))
            .CreateLogger();
    }

    public static IServiceCollection AddSeriLogConfiguration(this IServiceCollection serviceCollection)
    {
        SerilogConfiguration();
        serviceCollection.AddSingleton<ILogService, SeriLogService>();
        return serviceCollection;
    }
}