using System.Net;
using Common.JsonExtension;

namespace Common.Infrastructure.Net.Http;

public sealed class HttpClientFactory : Common.Application.NET.Http.IHttpClientFactory
{
    private readonly IHttpClientFactory _httpClient;


    public HttpClientFactory(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    ///     请求头参数集合
    ///     如携带Cookies或者一些其他信息
    ///     认证 Bearer
    ///
    /// </summary>
    private IDictionary<string, IDictionary<string, string>> HttpHeaderParams { get; set; } =
        new Dictionary<string, IDictionary<string, string>>();


    /// <summary>
    /// httpClient name
    /// </summary>
    /// <param name="name">http配置连接名</param>
    /// <param name="keyValuePair"></param>
    public void AddHeader(string name, KeyValuePair<string, string> keyValuePair)
    {
        if (HttpHeaderParams.TryGetValue(name, out var headderParams))
        {
            headderParams[keyValuePair.Key] = keyValuePair.Value;
        }
        else
        {
            headderParams = new Dictionary<string, string>();
            headderParams.Add(keyValuePair);
            HttpHeaderParams.Add(name, headderParams);
        }
    }

    public HttpClient TryGetInstance(string name = "")
    {
        var client = _httpClient.CreateClient(name);
        return client;
    }

    public HttpRequestMessage CreateRequest(string json, HttpMethod method, Uri uri)
    {
        var httpRequest = new HttpRequestMessage();
        httpRequest.Method = method;
        httpRequest.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        httpRequest.Headers.Add("Accept", "application/json");
        httpRequest.RequestUri = uri;
        return httpRequest;
    }

    public HttpRequestMessage CreateRequest(Uri uri, HttpMethod method)
    {
        var httpRequest = new HttpRequestMessage();
        httpRequest.Method = method;
        httpRequest.RequestUri = uri;
        return httpRequest;
    }

    /// <summary>
    /// 如 httpRequest.Headers.Add("Authorization", "Bearer ..."); 添加鉴权认证
    /// </summary>
    /// <param name="name"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    private HttpRequestMessage SetHttpRequsetHead(string name, HttpRequestMessage request)
    {
        if (HttpHeaderParams.TryGetValue(name, out var heads))
            foreach (var head in heads)
                request.Headers.Add(head.Key, head.Value);
        return request;
    }

    public async Task<HttpResponseMessage> GetAsync(string uri, string name = "")
    {
        var httpClient = TryGetInstance(name);
        var request = CreateRequest(new Uri(uri), HttpMethod.Get);
        SetHttpRequsetHead(name, request);
        var response = await httpClient.SendAsync(request);
        return response;
    }

    public async Task<T> GetAsync<T>(string uri, string name = "")
    {
        var response = await GetAsync(uri, name);
        var ins = default(T);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsStringAsync();
            ins = json.NewtonsoftParseJson<T>();
        }

        return ins;
    }

    public async Task<HttpResponseMessage> DeleteAsync(string uri, string name = "")
    {
        var httpClient = TryGetInstance(name);
        var request = CreateRequest(new Uri(uri), HttpMethod.Delete);
        SetHttpRequsetHead(name, request);
        var response = await httpClient.SendAsync(request);
        return response;
    }

    public async Task<T> DeleteAsync<T>(string uri, string name = "")
    {
        var response = await DeleteAsync(uri, name);
        var ins = default(T);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsStringAsync();
            ins = json.NewtonsoftParseJson<T>();
        }

        return ins;
    }

    public async Task<HttpResponseMessage> PostAsync(string uri, string name = "")
    {
        var httpClient = TryGetInstance(name);
        var request = CreateRequest(new Uri(uri), HttpMethod.Post);
        SetHttpRequsetHead(name, request);
        var response = await httpClient.SendAsync(request);
        return response;
    }

    public async Task<HttpResponseMessage> PostAsync<T>(string uri, T obj, string name = "")
    {
        var httpClient = TryGetInstance(name);
        var json = obj.NewtonsoftToJsonString();
        var request = CreateRequest(json, HttpMethod.Post, new Uri(uri));
        SetHttpRequsetHead(name, request);
        var response = await httpClient.SendAsync(request);
        return response;
    }

    public async Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest obj, string name = "")
    {
        var response = await PostAsync<TRequest>(uri, obj, name);
        var ins = default(TResult);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsStringAsync();
            ins = json.NewtonsoftParseJson<TResult>();
        }

        return ins;
    }
}