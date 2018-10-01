using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace SchoolExperienceServices.PerformancePlatform.Implementation
{
    internal class PerformancePlatformService : IPerformancePlatformService
    {
        public const string HttpClientName = "PerformancePlatform";

        private const string JsonMediaType = "application/json";

        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ILogger _logger;

        private readonly PerformancePlatformOptions _options;

        public PerformancePlatformService(
            IOptions<PerformancePlatformOptions> options,
            IHttpClientFactory httpClientFactory,
            ILoggerFactory loggerFactory)
        {
            _options = options.Value;
            _httpClientFactory = httpClientFactory;
            _logger = loggerFactory.CreateLogger(GetType());
        }

        public async Task EmptyAsync(string name)
        {
            var httpClient = GetHttpClient();

            var relativeUrl = $"/data/{_options.ServiceName}/{name}";
            var response = await httpClient.PostAsync(relativeUrl, new StringContent("[]", Encoding.UTF8, JsonMediaType));
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(string name, IEnumerable<PerformancePlatformMetric> metrics)
        {
            var httpClient = GetHttpClient();

            var relativeUrl = $"/data/{_options.ServiceName}/{name}";
            var jsonData = JsonConvert.SerializeObject(metrics);

            _logger.LogDebug($"PostAsync:{relativeUrl}->{jsonData}");

            var content = new StringContent(jsonData, Encoding.UTF8, JsonMediaType);
            var response = await httpClient.PostAsync(relativeUrl, content);
            response.EnsureSuccessStatusCode();
        }

        private HttpClient GetHttpClient()
        {
            var httpClient = _httpClientFactory.CreateClient(HttpClientName);
            httpClient.BaseAddress = new Uri(_options.WriteHost);
            httpClient.DefaultRequestHeaders.Add("Accept", JsonMediaType);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _options.BearerToken);

            return httpClient;
        }
    }
}
