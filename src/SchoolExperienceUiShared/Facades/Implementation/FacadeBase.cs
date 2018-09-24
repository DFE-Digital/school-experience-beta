using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SchoolExperienceUiShared.Facades.Implementation
{
    public class FacadeBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        public const string HttpClientName = "API";

        protected FacadeBase(IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = loggerFactory.CreateLogger(GetType());
        }

        protected async Task<TOut> GetStringAsync<TOut>(string relativeUri)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpClientName);

            try
            {
                var response = await httpClient.GetStringAsync(relativeUri);
                _logger.LogDebug($"GetStringAsyncResponse:{response}");

                return JsonConvert.DeserializeObject<TOut>(response);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Can't do: {relativeUri}");
                throw;
            }
        }

        class EmptyPost
        {
            public static EmptyPost Empty { get; } = new EmptyPost();
        }

        protected async Task<TOut> PostAsync<TIn, TOut>(string relativeUri, TIn data)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpClientName);

            var jsonData = JsonConvert.SerializeObject(data);

            _logger.LogDebug($"PostAsync:{relativeUri}->{jsonData}");

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var requestResult = await httpClient.PostAsync(relativeUri, content);
            var response = await requestResult.Content.ReadAsStringAsync();

            _logger.LogDebug($"PostAsyncResponse:{response}");

            return JsonConvert.DeserializeObject<TOut>(response);
        }

        protected async Task<TOut> DeleteAsync<TOut>(string relativeUri)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpClientName);

            _logger.LogDebug($"DeleteAsync:{relativeUri}");

            var requestResult = await httpClient.DeleteAsync(relativeUri);
            var response = await requestResult.Content.ReadAsStringAsync();

            _logger.LogDebug($"DeleteAsyncResponse:{response}");

            return JsonConvert.DeserializeObject<TOut>(response);
        }
    }
}
