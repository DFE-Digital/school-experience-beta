using System;
using System.Globalization;
using FluentAssertions;
using NUnit.Framework;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceBaseTypesTests
{
    [TestFixture]
    public class DateTimeExtensionTests
    {
        [TestCase("2018-08-01", "2018-08-01")]
        [TestCase("2018-08-02", "2018-08-01")]
        [TestCase("2018-08-31", "2018-08-01")]
        [TestCase("2018-12-31", "2018-12-01")]
        public void StartOfMonth(string testDateString, string expectedDateString)
        {
            // Arrange
            var testDate = DateTime.Parse(testDateString, CultureInfo.InvariantCulture);
            var expectedResult = DateTime.Parse(expectedDateString, CultureInfo.InvariantCulture);

            // Act
            var result = testDate.StartOfMonth();

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestCase("2018-08-01", "2018-08-31T23:59:59.999")]
        [TestCase("2018-08-02", "2018-08-31T23:59:59.999")]
        [TestCase("2018-08-31", "2018-08-31T23:59:59.999")]
        [TestCase("2018-12-31", "2018-12-31T23:59:59.999")]
        public void EndOfMonth(string testDateString, string expectedDateString)
        {
            // Arrange
            var testDate = DateTime.Parse(testDateString, CultureInfo.InvariantCulture);
            var expectedResult = DateTime.Parse(expectedDateString, CultureInfo.InvariantCulture);

            // Act
            var result = testDate.EndOfMonth();

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}