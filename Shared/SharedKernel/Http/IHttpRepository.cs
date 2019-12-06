using System.Threading.Tasks;

namespace SharedKernel.Http
{
    public interface IHttpRepository
    {
        Task<string> GetAsync(string endpoint, string authToken);
        Task<T> GetAsync<T>(string endpoint, string authToken) where T : class;
        Task<string> GetAsyncAsString(string endpoint, string authToken);
        Task<string> Post<T>(T data, string endpoint, string authToken);
        Task<U> Post<U, T>(T data, string endpoint, string authToken) where U : class;
        Task<string> Put<T>(T data, string endpoint, string authToken);
        Task<U> Put<U, T>(T data, string endpoint, string authToken) where U : class;
    }
}