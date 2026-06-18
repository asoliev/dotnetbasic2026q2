using System;
using FluentAssertions;
using Xunit;

namespace BinaryGap_5;

public class BinaryGapTests
{
    [Theory]
    [InlineData(9, 2)]
    [InlineData(529, 4)]
    [InlineData(20, 1)]
    [InlineData(15, 0)]
    [InlineData(1041, 5)]
    public void Solution_Should_Return_Expected_Result(int number, int expected)
    {
        int actual = BinaryGap.Solution(number);

        actual.Should().Be(expected);
    }
}