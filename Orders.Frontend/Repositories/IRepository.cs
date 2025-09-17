using Orders.Shared.Responses;

namespace Orders.Frontend.Repositories
{
    public interface IRepository
    {
        Task<HttpResponseWrapper<T>> GetAsync<T>(string url);
        Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model);
        Task<HttpResponseWrapper<ActionResponse>> PostAsync<T, ActionResponse>(string url, T model);
        Task<HttpResponseWrapper<object>> DeleteAsync<T>(string url);
        Task<HttpResponseWrapper<object>> PutAsync<T>(string url, T model);
        Task<HttpResponseWrapper<ActionResponse>> PutAsync<T, ActionResponse>(string url, T model);
    }
}
