using System;
using FluentAssertions;
using Xunit;

namespace OddEven_20;

public class OddEvenTests
{
    [Theory]
    [InlineData(2, "Even")]
    [InlineData(9, "Odd")]
    [InlineData(3, "3")]
    [InlineData(11, "11")]
    public void Classify_Should_Return_Expected_Result(int number, string expected)
    {
        string actual = OddEven.Classify(number);

        actual.Should().Be(expected);
    }
}