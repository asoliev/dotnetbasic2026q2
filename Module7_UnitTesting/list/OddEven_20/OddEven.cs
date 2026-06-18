using System;
namespace OddEven_20;

public static class OddEven
{
    public static string Classify(int number)
    {
        if (number < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(number));
        }

        if (number % 2 == 0)
        {
            return "Even";
        }

        if (IsPrime(number))
        {
            return number.ToString();
        }

        return "Odd";
    }

    private static bool IsPrime(int number)
    {
        if (number < 2)
        {
            return false;
        }

        for (int divisor = 2; divisor * divisor <= number; divisor++)
        {
            if (number % divisor == 0)
            {
                return false;
            }
        }

        return true;
    }
}