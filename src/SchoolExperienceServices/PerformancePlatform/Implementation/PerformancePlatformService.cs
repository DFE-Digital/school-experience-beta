using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SchoolExperienceServices.PerformancePlatform;

namespace SchoolExperienceServices.PerformancePlatform.Implementation
{
    internal class PerformancePlatformService : IPerformancePlatformService
    {
        public const string HttpClientName = "PerformancePlatform";

        private readonly PerformancePlatformOptions _options;
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ILogger _logger;

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
            var response = await httpClient.PostAsync(relativeUrl, new StringContent("[]"));
            response.EnsureSuccessStatusCode();

        }

        public async Task UpdateAsync(string name, IEnumerable<PerformancePlatformMetric> metrics)
        {
            var httpClient = GetHttpClient();

            var relativeUrl = $"/data/{_options.ServiceName}/{name}";
            var response = await httpClient.PostAsJsonAsync(relativeUrl, metrics);
            response.EnsureSuccessStatusCode();
        }

        private HttpClient GetHttpClient()
        {
            var httpClient = _httpClientFactory.CreateClient(HttpClientName);
            httpClient.BaseAddress = new Uri(_options.WriteHost);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _options.BearerToken);

            return httpClient;
        }
    }
}
