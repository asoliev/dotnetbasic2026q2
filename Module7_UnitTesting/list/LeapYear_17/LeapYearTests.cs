using FluentAssertions;
using Xunit;

namespace LeapYear_17;

public class LeapYearTests
{
    [Theory]
    [InlineData(1996, true)]
    [InlineData(2000, true)]
    [InlineData(1900, false)]
    [InlineData(2001, false)]
    public void IsLeapYear_Should_Return_Expected_Result(int year, bool expected)
    {
        bool actual = LeapYear.IsLeapYear(year);

        actual.Should().Be(expected);
    }
}