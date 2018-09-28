using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using SchoolExperienceTestHelpers;
using SchoolExperienceUi.Facades.Implementation;

namespace SchoolExperienceUiTests
{
    [TestFixture]
    public class SchoolFacadeTests
    {
        [Test]
        public void Test()
        {
            // Arrange
            var expectedResponse = "{\"searchDistance\":{\"metres\":1609.0},\"schools\":[{\"schoolId\":\"1A\",\"name\":\"SchoolName\",\"contactName\":\"ContactName\",\"address\":\"Address\",\"distance\":{\"metres\":1500.0},\"schoolType\":1}]}";

            var requestUri = new Uri("http://localhost:5001/api/v1/school/findschools");

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(expectedResponse) };

            var handler = new Mock<HttpClientHandler>();
            handler.Protected()
                //.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(message => message.RequestUri.ToString().StartsWith(requestUri.ToString())), ItExpr.IsAny<CancellationToken>())
                .Callback((HttpRequestMessage message, CancellationToken token) => Debug.WriteLine(message.RequestUri))
                .ReturnsAsync(responseMessage);

            var httpClient = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("http://localhost:5001/api/")
            };

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var loggerFactory = new MockLoggerFactory();

            var sut = new SchoolFacade(httpClientFactory.Object, loggerFactory.Object);

            // Act
            var result = sut.FindSchoolsAsync("OL158RE", new SchoolExperienceBaseTypes.Distance { Miles = 1 }).Result;

            // Assert
            result.SearchDistance.Metres.Should().Be(1609);
            result.Schools.Count().Should().Be(1);
            var school = result.Schools.First();
            school.SchoolId.Should().Be("1A");
            school.Name.Should().Be("SchoolName");
            school.Address.Should().Be("Address");
            school.ContactName.Should().Be("ContactName");
            school.Distance.Metres.Should().Be(1500);
            school.SchoolType.Should().Be(SchoolExperienceBaseTypes.SchoolType.Primary);
        }
    }
}
