using FluentAssertions;
using Xunit;

namespace FizzBuzz_12;

public class FizzBuzzTests
{
    [Theory]
    [InlineData(1, "1")]
    [InlineData(3, "Fizz")]
    [InlineData(5, "Buzz")]
    [InlineData(15, "FizzBuzz")]
    public void GetValue_Should_Return_Expected_Result(int number, string expected)
    {
        string actual = FizzBuzz.GetValue(number);

        actual.Should().Be(expected);
    }
}