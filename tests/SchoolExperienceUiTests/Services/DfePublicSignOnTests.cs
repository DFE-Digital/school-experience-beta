using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using SchoolExperienceUi.Services;

namespace SchoolExperienceUiTests.Services
{
    [TestFixture]
    public class DfePublicSignOnTests
    {
        private Mock<IHttpClientFactory> _mockHttpClientFactory;
        private Mock<IOptions<DfePublicSignOnOptions>> _mockOptions;
        private MockRepository _mockRepository;

        [Test]
        public async Task CreateAccount_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var clientToken = "CLIENTTOKEN";
            var callbackUrl = new Uri("http://cb");
            var organisation = "ORGANISATION";
            var serviceUrl = new Uri("http://ServiceUrl");
            var userRedirectUrl = new Uri("http://UserRedirect");
            string jwtToken = "TOKEN";
            string clientId = "CLIENTID";
            string emailAddress = "EMAILADDRESS";
            string givenName = "GIVENNAME";
            string familyName = "FAMILYNAME";

            HttpRequestMessage httpRequestMessage = null;
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent($"{{'sourceId':'{clientId}', 'sub':'{clientToken}'}}")
            };
            responseMessage.Headers.Add("authorization", "bearer TOKEN");
            SetupHttpClient(new Uri("http://ServiceUrl"), (message) => httpRequestMessage = message, responseMessage);

            var sut = CreateDfePublicSignOn(new DfePublicSignOnOptions
            {
                CallbackUrl = callbackUrl,
                Organisation = organisation,
                ServiceUrl = serviceUrl,
                UserRedirect = userRedirectUrl,
            });


            // Act
            var result = await sut.CreateAccount(
                jwtToken,
                clientId,
                emailAddress,
                givenName,
                familyName);

            // Assert
            result.JwtToken.Should().Be(jwtToken);
            result.ClientId.Should().Be(clientId);
            result.ClientToken.Should().Be(clientToken);

            httpRequestMessage.Should().NotBeNull();
            httpRequestMessage.Headers.Authorization.Scheme.Should().Be("bearer");
            httpRequestMessage.Headers.Authorization.Parameter.Should().Be(jwtToken);
            httpRequestMessage.Method.Should().Be(HttpMethod.Post);
            httpRequestMessage.RequestUri.Should().Be(serviceUrl);

            var requestMessage = httpRequestMessage.Content.ReadAsStringAsync().Result;
            var requestData = JsonConvert.DeserializeObject<DfeSignInRequest>(requestMessage);
            requestData.Callback.Should().Be(callbackUrl);
            requestData.Email.Should().Be(emailAddress);
            requestData.FamilyName.Should().Be(familyName);
            requestData.GivenName.Should().Be(givenName);
            requestData.InviteBodyOverride.Should().BeNullOrEmpty();
            requestData.InviteSubjectOverride.Should().BeNullOrEmpty();
            requestData.Organisation.Should().Be(organisation);
            requestData.SourceId.Should().Be(clientId);
            requestData.UserRedirect.Should().Be(userRedirectUrl);
        }

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockOptions = _mockRepository.Create<IOptions<DfePublicSignOnOptions>>();
            _mockHttpClientFactory = _mockRepository.Create<IHttpClientFactory>();
        }

        [TearDown]
        public void TearDown()
        {
            _mockRepository.VerifyAll();
        }

        private DfePublicSignOn CreateDfePublicSignOn(DfePublicSignOnOptions options)
        {
            _mockOptions
                .SetupGet(x => x.Value)
                .Returns(options);

            return new DfePublicSignOn(
                 _mockHttpClientFactory.Object,
                 _mockOptions.Object);
        }

        private void SetupHttpClient(Uri requestUri, Action<HttpRequestMessage> requestMessage, HttpResponseMessage responseMessage)
        {
            var handler = new Mock<HttpClientHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(nameof(HttpClient.SendAsync), ItExpr.Is<HttpRequestMessage>(message => message.RequestUri.ToString().StartsWith(requestUri.ToString())), ItExpr.IsAny<CancellationToken>())
                .Callback((HttpRequestMessage message, CancellationToken token) => requestMessage(message))
                .ReturnsAsync(responseMessage);

            var httpClient = new HttpClient(handler.Object);
            _mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
        }
    }
}
