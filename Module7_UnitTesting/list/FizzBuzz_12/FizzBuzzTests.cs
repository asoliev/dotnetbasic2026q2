using FluentAssertions;
using Xunit;

namespace FizzBuzz_12;

public class FizzBuzzTests
{
    [Fact]
    public void GetValue_Should_Return_Number_As_String()
    {
        string actual = FizzBuzz.GetValue(1);

        actual.Should().Be("1");
    }

    [Fact]
    public void GetValue_Should_Return_Fizz_For_Multiples_Of_Three()
    {
        string actual = FizzBuzz.GetValue(3);

        actual.Should().Be("Fizz");
    }

    [Fact]
    public void GetValue_Should_Return_Buzz_For_Multiples_Of_Five()
    {
        string actual = FizzBuzz.GetValue(5);

        actual.Should().Be("Buzz");
    }

    [Fact]
    public void GetValue_Should_Return_FizzBuzz_For_Multiples_Of_Three_And_Five()
    {
        string actual = FizzBuzz.GetValue(15);

        actual.Should().Be("FizzBuzz");
    }
}