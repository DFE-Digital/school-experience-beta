using FluentAssertions;
using NUnit.Framework;
using SchoolExperienceUi.Attributes.Validation;

namespace SchoolExperienceUiTests.Attributes.Validation
{
    [TestFixture]
    public class PostCodeAttributeTests
    {
        [TestCase("A1")]
        [TestCase("A11")]
        [TestCase("A11A")]
        [TestCase("A1 1A")]
        public void InvalidPostCodes(string postCode)
        {
            // Arrange
            var sut = new PostCodeAttribute();

            // Act
            var result = sut.IsValid(postCode);

            // Assert
            result.Should().BeFalse();
        }

        [TestCase("OL158RE")]
        [TestCase("A11AB")]
        [TestCase("A111AB")]
        public void ValidPostCodes(string postCode)
        {
            // Arrange
            var sut = new PostCodeAttribute();

            // Act
            var result = sut.IsValid(postCode);

            // Assert
            result.Should().BeTrue();
        }
    }
}
