namespace Common.Application.NetWork.Http;

public interface IHttpClientFactory
{
    public void AddHeader(string name, KeyValuePair<string, string> keyValuePair);

    public HttpClient TryGetInstance(string name = "");

    public HttpRequestMessage CreateRequest(string josn, HttpMethod method, Uri uri);

    public HttpRequestMessage CreateRequest(Uri uri, HttpMethod method);

    public Task<HttpResponseMessage> GetAsync(string uri, string name = "");

    public Task<T> GetAsync<T>(string uri, string name = "");

    public Task<HttpResponseMessage> DeleteAsync(string uri, string name = "");

    public Task<T> DeleteAsync<T>(string uri, string name = "");
    public Task<HttpResponseMessage> PostAsync(string uri, string name = "");

    public Task<HttpResponseMessage> PostAsync<T>(string uri, T obj, string name = "");

    public Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest obj, string name = "");
}