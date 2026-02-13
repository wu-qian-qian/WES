using Common.Application.NetWork.Other.Base;
using Common.Infrastructure.NetWork.Http;
using Common.Infrastructure.NetWork.Other;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace Common.Infrastructure.NetWork;

public static class NetConfiguration
{
    public static IServiceCollection AddHttpConfiguration(this IServiceCollection serviceCollection,
        HttpOptions options = null)
    {
        if (options != null)
        {
            var httpClientBuilder = serviceCollection.AddHttpClient(options.Name, client =>
            {
                client.BaseAddress = new Uri(options.BaseAddress);
                client.Timeout = TimeSpan.FromMilliseconds(options.Timeout);
            });

            if (options.HttpLogHandler != null)
                httpClientBuilder.AddHttpMessageHandler(_ => new HttpLoggingHandler(options.HttpLogHandler));
            if (options.EnablePolicy)
            {
                // Handle HTTP 5xx errors or HTTP 408 requests  状态码这几个重试
                var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                    //重试的配置注册
                    .WaitAndRetryAsync(
                        options.RetryCount, // 重试次数
                        attempt => TimeSpan.FromSeconds(options.RetryDelay), // 重试间隔时间
                        (outcome, timespan, retryAttempt, context) =>
                        {
                            options.PolicCallback?.Invoke(outcome.Result);
                        });
                httpClientBuilder.AddPolicyHandler(retryPolicy);
            }
        }
        else
        {
            serviceCollection.AddHttpClient();
        }
        return serviceCollection;
    }

    public static IServiceCollection AddNetClientConfiguration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<INetService, NetService>();
        return serviceCollection;
    }
}