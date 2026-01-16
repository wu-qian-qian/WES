using System.Text;
using Common.Encoding;

namespace Common.Infrastructure.NetWork.Http;

public class HttpLoggingHandler : DelegatingHandler
{
    private readonly Action<string> _logAction;

    public HttpLoggingHandler(Action<string> logAction) : base(new HttpClientHandler())
    {
        _logAction = logAction ?? throw new ArgumentNullException(nameof(logAction));
    }

    private void WriteLine(string message)
    {
        _logAction(message);
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Method: {request.Method}");
        sb.AppendLine($"URL: {request.RequestUri}");
        sb.AppendLine("Request Headers:");
        foreach (var header in request.Headers) sb.AppendLine($"  {header.Key}: {string.Join(", ", header.Value)}");

        if (request.Content != null)
        {
            var requestMediaType = request.Content.Headers.ContentType?.MediaType;
            var requestCharset = request.Content.Headers.ContentType?.CharSet;
            var requestEncoding = EncodingHelper.GetEncodingFromContentType(requestCharset);
            sb.AppendLine("Request Content Headers:");
            foreach (var header in request.Content.Headers)
                sb.AppendLine($"  {header.Key}: {string.Join(", ", header.Value)}");
            //HTTP Content 数据是一次性所以读取后需要重新赋值
            if (string.IsNullOrWhiteSpace(requestMediaType))
            {
                sb.AppendLine("Null or empty Content-Type header.");
                // Output raw body content
                var rawBody = await request.Content.ReadAsStringAsync();
                sb.AppendLine($"Raw Body: {rawBody}");
                request.Content = new StringContent(rawBody, requestEncoding);
            }
            else if (IsTextMediaType(requestMediaType))
            {
                var requestBody = await request.Content.ReadAsStringAsync();
                sb.AppendLine($"Body: {requestBody}");
                request.Content =
                    new StringContent(requestBody, requestEncoding, requestMediaType);
            }
            // TODO
        }
        else
        {
            sb.AppendLine("Body: Empty Content");
        }

        var response = await base.SendAsync(request, cancellationToken);
        sb.AppendLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Response:");
        sb.AppendLine($"Status Code: {(int)response.StatusCode} {response.StatusCode}");
        var respContentType = response.Content.Headers.ContentType;
        var respMediaType = respContentType?.MediaType;
        var respCharset = respContentType?.CharSet;
        var respEncoding = EncodingHelper.GetEncodingFromContentType(respCharset);
        if (IsTextMediaType(respMediaType))
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            sb.AppendLine($"Body: {responseBody}");
            response.Content = new StringContent(responseBody, respEncoding, respMediaType);
        }

        sb.AppendLine(new string('*', 50));
        WriteLine(sb.ToString());
        return response;
    }

    private static bool IsTextMediaType(string requestMediaType)
    {
        return requestMediaType.StartsWith("text/")
               || requestMediaType == "application/json" || requestMediaType.EndsWith("+json");
    }
}