using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace SchoolExperienceUi.Services
{
    public partial class DfePublicSignOn
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public const string HttpClientFactoryName = "DfePublicSignOn";

        private readonly DfePublicSignOnOptions _options;

        public DfePublicSignOn(IHttpClientFactory httpClientFactory, IOptions<DfePublicSignOnOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _options = options.Value;
        }

        public async Task<CreateAccountResult> CreateAccount(string jwtToken, string clientId, string emailAddress, string givenName, string familyName)
        {
            var client = _httpClientFactory.CreateClient(HttpClientFactoryName);

            var request = new DfeSignInRequest
            {
                Email = emailAddress,
                GivenName = givenName,
                FamilyName = familyName,
                Callback = _options.CallbackUrl,
                SourceId = clientId,
                UserRedirect = _options.UserRedirect,
                Organisation = _options.Organisation,
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(request,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            }));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", jwtToken);

            var result = await client.PostAsync(_options.ServiceUrl, requestContent).ConfigureAwait(false);
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            var response = JsonConvert.DeserializeObject<DfeSignInResponse>(content);

            return new CreateAccountResult
            {
                JwtToken = GetHeader(result.Headers, "authorization", "bearer "),
                ClientId = response.SourceId,
                ClientToken = response.Sub,
            };
        }

        private string GetHeader(HttpResponseHeaders headers, string header, string startsWith)
        {
            if (headers.TryGetValues(header, out var items))
            {
                foreach (var item in items)
                {
                    if (item.StartsWith(startsWith))
                    {
                        return item.Substring(startsWith.Length);
                    }
                }
            }

            return null;
        }
    }
}

