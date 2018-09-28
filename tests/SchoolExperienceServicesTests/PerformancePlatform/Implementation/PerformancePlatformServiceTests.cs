using System;
using System.Collections.Generic;
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
using SchoolExperienceServices.PerformancePlatform;
using SchoolExperienceServices.PerformancePlatform.Implementation;
using SchoolExperienceTestHelpers;

namespace SchoolExperienceServicesTests
{
    [TestFixture]
    public class PerformancePlatformServiceTests
    {
        [Test]
        public void UpdateAsync()
        {
            // Arrange
            var expectedResponse = @"{""Status"":""OK""}";

            var requestUri = new Uri("http://localhost:5001/data/SERVICE/Test");

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(expectedResponse) };

            HttpRequestMessage httpRequestMessage = null;

            var handler = new Mock<HttpClientHandler>();
            handler.Protected()
                //.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(message => message.RequestUri.ToString().StartsWith(requestUri.ToString())), ItExpr.IsAny<CancellationToken>())
                .Callback((HttpRequestMessage message, CancellationToken token) => httpRequestMessage = message)
                .ReturnsAsync(responseMessage);

            var httpClient = new HttpClient(handler.Object);
            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var loggerFactory = new MockLoggerFactory();

            var options = new Mock<IOptions<PerformancePlatformOptions>>();
            options.SetupGet(x => x.Value).Returns(new PerformancePlatformOptions
            {
                BearerToken = "TOKEN",
                ServiceName = "SERVICE",
                WriteHost = "http://localhost:5001"
            });

            var sut = new PerformancePlatformService(options.Object, httpClientFactory.Object, loggerFactory.Object);

            // Act
            var metrics = new List<PerformancePlatformMetric>
            {
                new PerformancePlatformMetric { Id = "1", When = new DateTime(2018, 9, 28, 12, 0, 0, DateTimeKind.Utc) },
                new PerformancePlatformMetric { Id = "2", When = new DateTime(2018, 9, 27, 11, 59, 59, DateTimeKind.Utc) },
            };

            sut.UpdateAsync("Test", metrics).Wait();

            // Assert
            httpRequestMessage.Should().NotBeNull();
            httpRequestMessage.Method.Method.Should().Be("POST");
            httpRequestMessage.RequestUri.AbsoluteUri.Should().Be("http://localhost:5001/data/SERVICE/Test");
            httpRequestMessage.Headers.Authorization.Scheme.Should().Be("bearer");
            httpRequestMessage.Headers.Authorization.Parameter.Should().Be("TOKEN");
            httpRequestMessage.Headers.Accept.Should().Contain(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var sentContent = JsonConvert.DeserializeObject<List<PerformancePlatformMetric>>(httpRequestMessage.Content.ReadAsStringAsync().Result);
            sentContent.Should().BeEquivalentTo(metrics);
        }

        [Test]
        public void EmptyAsync()
        {
            // Arrange
            var expectedResponse = @"{""Status"":""OK""}";

            var requestUri = new Uri("http://localhost:5001/data/SERVICE/Test");

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(expectedResponse) };

            HttpRequestMessage httpRequestMessage = null;

            var handler = new Mock<HttpClientHandler>();
            handler.Protected()
                //.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(message => message.RequestUri.ToString().StartsWith(requestUri.ToString())), ItExpr.IsAny<CancellationToken>())
                .Callback((HttpRequestMessage message, CancellationToken token) => httpRequestMessage = message)
                .ReturnsAsync(responseMessage);

            var httpClient = new HttpClient(handler.Object);
            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var loggerFactory = new MockLoggerFactory();

            var options = new Mock<IOptions<PerformancePlatformOptions>>();
            options.SetupGet(x => x.Value).Returns(new PerformancePlatformOptions
            {
                BearerToken = "TOKEN",
                ServiceName = "SERVICE",
                WriteHost = "http://localhost:5001"
            });

            var sut = new PerformancePlatformService(options.Object, httpClientFactory.Object, loggerFactory.Object);

            // Act
            sut.EmptyAsync("Test").Wait();

            // Assert
            httpRequestMessage.Should().NotBeNull();
            httpRequestMessage.Method.Method.Should().Be("POST");
            httpRequestMessage.RequestUri.AbsoluteUri.Should().Be("http://localhost:5001/data/SERVICE/Test");
            httpRequestMessage.Headers.Authorization.Scheme.Should().Be("bearer");
            httpRequestMessage.Headers.Authorization.Parameter.Should().Be("TOKEN");
            httpRequestMessage.Headers.Accept.Should().Contain(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var sentContent = JsonConvert.DeserializeObject<List<PerformancePlatformMetric>>(httpRequestMessage.Content.ReadAsStringAsync().Result);
            sentContent.Should().BeEmpty();
        }
    }
}
