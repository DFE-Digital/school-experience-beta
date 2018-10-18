using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SchoolExperienceCandidateDataServices.Implementation;

namespace SchoolExperienceCandidateDataServicesTests.Implementation
{
    [TestFixture]
    public class GitisServicesTests
    {
        private Mock<IOptions<GitisServicesOptions>> _mockOptions;
        private MockRepository _mockRepository;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _mockOptions = _mockRepository.Create<IOptions<GitisServicesOptions>>();
        }

        [Test]
        public async Task SignIn_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sut = CreateGitisServices(new GitisServicesOptions
            {
                ServiceUri = new Uri("http://services.odata.org/V4/OData/OData.svc/"),
                UserName = "abc",
                Password = "123",
            });
            var credentials = "Jose Pavarotti";

            // Act
            var result = await sut.SignIn(credentials);

            // Assert
            result.Id.Should().Be("1");
        }

        [TearDown]
        public void TearDown()
        {
            _mockRepository.VerifyAll();
        }

        [Test]
        public async Task UpdateName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sut = CreateGitisServices(new GitisServicesOptions
            {
                ServiceUri = new Uri("http://services.odata.org/V4/OData/OData.svc/"),
                UserName = "abc",
                Password = "123",
            });
            var id = "1";
            var newName = "test-candidate";

            // Act
            var result = await sut.UpdateName(
                id,
                newName);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        private GitisServices CreateGitisServices(GitisServicesOptions options)
        {
            _mockOptions.SetupGet(x => x.Value).Returns(options);
            return new GitisServices(_mockOptions.Object);
        }
    }
}
