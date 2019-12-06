using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SharedKernel.Http
{
    public class HttpRepository
    {
        protected readonly HttpClient _httpClient;

        public HttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public virtual async  Task<string> Put<T>(T data, string endpoint, string authToken)
        {
            if (!string.IsNullOrWhiteSpace(authToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            var jsonContent = new JsonContent(data);

            var result = await _httpClient.PutAsync(endpoint, jsonContent).ConfigureAwait(false);

            if (!result.IsSuccessStatusCode)
                throw new Exception($"Failed to Update Url: '{ endpoint }' Token: '{authToken}' {result.ReasonPhrase}");

            return await result.Content.ReadAsStringAsync();
        }

        public virtual async Task<U> Put<U, T>(T data, string endpoint, string authToken) where U : class
        {
            var responseString = await this.Put<T>(data, endpoint, authToken);

            var jsonDataResponse = JsonConvert.DeserializeObject<U>(responseString);

            return jsonDataResponse;
        }

        public virtual async Task<string> Post<T>(T data, string endpoint, string authToken)
        {
            if (!string.IsNullOrWhiteSpace(authToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            var jsonContent = new JsonContent(data);

            var result = await _httpClient.PostAsync(endpoint, jsonContent).ConfigureAwait(false);

            if (!result.IsSuccessStatusCode)
                throw new Exception($"Failed to Update Url: '{ endpoint }' Token: '{authToken}' {result.ReasonPhrase}");

            return await result.Content.ReadAsStringAsync();
        }

        public virtual async Task<U> Post<U, T>(T data, string endpoint, string authToken) where U : class
        {
            var responseString = await this.Post<T>(data, endpoint, authToken);

            var jsonDataResponse = JsonConvert.DeserializeObject<U>(responseString);

            return jsonDataResponse;
        }

        public virtual async Task<string> GetAsync(string endpoint, string authToken)
        {
            if (!string.IsNullOrWhiteSpace(authToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            var result = await _httpClient.GetAsync(endpoint);

            return await result.Content.ReadAsStringAsync();
        }

        public virtual async Task<T> GetAsync<T>(string endpoint, string authToken) where T : class
        {
            var responseString = await this.GetAsync(endpoint, authToken);

            var jsonResponse = JsonConvert.DeserializeObject<T>(responseString);

            return jsonResponse;
        }

        public virtual async Task<string> GetAsyncAsString(string endpoint, string authToken)
        {
            var responseString = await this.GetAsync(endpoint, authToken);

            return responseString;
        }

    }
}
