using System;
using FluentAssertions;
using Xunit;

namespace OddEven_20;

public class OddEvenTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Classify_Should_Throw_For_Invalid_Input(int number)
    {
        Action act = () => OddEven.Classify(number);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(1, "1")]
    [InlineData(2, "Even")]
    [InlineData(9, "Odd")]
    [InlineData(3, "3")]
    [InlineData(11, "11")]
    [InlineData(15, "Odd")]
    [InlineData(17, "17")]
    [InlineData(8, "Even")]
    public void Classify_Should_Return_Expected_Result(int number, string expected)
    {
        string actual = OddEven.Classify(number);

        actual.Should().Be(expected);
    }
}