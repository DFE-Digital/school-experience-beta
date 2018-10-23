using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SchoolExperienceUi.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using FluentAssertions;
using System.Linq;

namespace SchoolExperienceUiTests.Services
{
    [TestFixture]
    public class DfePublicSignInJwtAuthenticationTests
    {
        private MockRepository _mockRepository;

        private Mock<IOptions<DfePublicSignInJwtAuthenticationOptions>> _mockOptions;

        [SetUp]
        public void SetUp()
        {
             _mockRepository = new MockRepository(MockBehavior.Strict);

             _mockOptions =  _mockRepository.Create<IOptions<DfePublicSignInJwtAuthenticationOptions>>();
        }

        [TearDown]
        public void TearDown()
        {
             _mockRepository.VerifyAll();
        }

        private DfePublicSignInJwtAuthentication CreateDfePublicSignInJwtAuthentication(DfePublicSignInJwtAuthenticationOptions options)
        {
            _mockOptions.SetupGet(x => x.Value).Returns(options);
            return new DfePublicSignInJwtAuthentication(_mockOptions.Object);
        }

        [Test]
        public void GenerateToken_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sut = CreateDfePublicSignInJwtAuthentication(new DfePublicSignInJwtAuthenticationOptions
            {
                ClientSecret = "SECRETSECRETSECRETSECRETSECRETSECRETSECRETSECRET",
                TokenExpiresAfter = TimeSpan.FromDays(5000),
            });
            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim("name", "value"),
            };

            // Act
            var result = sut.GenerateToken(claims);

            // Assert
            var expectedResult = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoidmFsdWUiLCJuYmYiOjE1NDAzMDY0NTIsImV4cCI6MTk3MjMwNjQ1MiwiaXNzIjoiU2Nob29sRXhwZXJpZW5jZUlzc3VlciIsImF1ZCI6IlNjaG9vbEV4cGVyaWVuY2VDb25zdW1lciJ9.8QbLl-CSBrfxvKw9NW3W6Gx1LUh6NT9FN10c6DSOnIw";
            result.Should().Be(expectedResult);
        }

        [Test]
        public void VerifyToken_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = CreateDfePublicSignInJwtAuthentication(new DfePublicSignInJwtAuthenticationOptions
            {
                ClientSecret = "SECRETSECRETSECRETSECRETSECRETSECRETSECRETSECRET",
                TokenExpiresAfter = TimeSpan.FromDays(5000),
            });
            string token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoidmFsdWUiLCJuYmYiOjE1NDAzMDY0NTIsImV4cCI6MTk3MjMwNjQ1MiwiaXNzIjoiU2Nob29sRXhwZXJpZW5jZUlzc3VlciIsImF1ZCI6IlNjaG9vbEV4cGVyaWVuY2VDb25zdW1lciJ9.8QbLl-CSBrfxvKw9NW3W6Gx1LUh6NT9FN10c6DSOnIw";

            // Act
            var result = unitUnderTest.VerifyToken(token).ToList();

            // Assert
            result.Should().NotBeNull();
            var firstClaim = result.First(x=>x.Type == "name");
            firstClaim.Value.Should().Be("value");
        }
    }
}
