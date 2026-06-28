using System;
using FluentAssertions;
using Xunit;

namespace BinaryGap_5;

public class BinaryGapTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Solution_Should_Throw_For_Invalid_Input(int number)
    {
        Action act = () => BinaryGap.Solution(number);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(9, 2)]
    [InlineData(529, 4)]
    [InlineData(20, 1)]
    [InlineData(15, 0)]
    [InlineData(1041, 5)]
    [InlineData(561892, 3)]
    public void Solution_Should_Return_Expected_Result(int number, int expected)
    {
        int actual = BinaryGap.Solution(number);

        actual.Should().Be(expected);
    }
}