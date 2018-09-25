using FluentAssertions;
using NUnit.Framework;
using SchoolExperienceHelpers;

namespace SchoolExperienceHelpersTests.Email
{
    [TestFixture]
    public class EmailHelpersTests
    {
        [TestCase("abcd.efgh@xxx.com", "a*d.e*h@xxx.com")]
        [TestCase("abc.def@xxx.com", "a*c.d*f@xxx.com")]
        [TestCase("ab.cde@xxx.com", "*.c*e@xxx.com")]
        [TestCase("ab.cd@xxx.com", "*.*@xxx.com")]
        [TestCase("abc@xxx.com", "a*c@xxx.com")]
        [TestCase("ac@xxx.com", "*@xxx.com")]
        [TestCase("a@xxx.com", "*@xxx.com")]
        [TestCase("abcd-efgh@xxx.com", "a*h@xxx.com")]
        [TestCase("abc-def@xxx.com", "a*f@xxx.com")]
        [TestCase("ab-cde@xxx.com", "a*e@xxx.com")]
        public void Test1(string emailAddress, string expectedResult)
        {
            // Arrange

            // Act
            var result = EmailHelpers.AnonymiseEmailAddress(emailAddress);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
