using System;
using FluentAssertions;
using NUnit.Framework;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceBaseTypesTests
{
    [TestFixture]
    public class DistanceTests
    {
        [TestCase(1, 1000)]
        [TestCase(10, 10000)]
        public void Kilometres(int kilometres, int expectedMetres)
        {
            // Arrange
            var sut = new Distance
            {
                Kilometres = kilometres
            };

            // Act
            var result = sut.Metres;

            // Assert
            result.Should().BeApproximately(expectedMetres, 2);
        }

        [TestCase(1, 1609)]
        [TestCase(5, 8047)]
        public void Miles(int miles, int expectedMetres)
        {
            // Arrange
            var sut = new Distance
            {
                Miles = miles
            };

            // Act
            var result = sut.Metres;

            // Assert
            result.Should().BeApproximately(expectedMetres, 1);
        }
    }
}
