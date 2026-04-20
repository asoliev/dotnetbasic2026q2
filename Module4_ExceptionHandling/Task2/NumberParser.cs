using System;

namespace Task2;

public class NumberParser : INumberParser
{
    public int Parse(string stringValue)
    {
        ArgumentNullException.ThrowIfNull(stringValue);

        // Trim trailing whitespace (tests show trailing spaces are allowed)
        string trimmed = stringValue.TrimEnd();

        if (trimmed.Length == 0)
            throw new FormatException("Input string was not in a correct format.");

        int index = 0;
        bool negative = false;

        if (trimmed[0] == '-')
        {
            negative = true;
            index = 1;
        }
        else if (trimmed[0] == '+')
        {
            index = 1;
        }

        if (index >= trimmed.Length)
            throw new FormatException("Input string was not in a correct format.");

        long result = 0;
        bool hasDigit = false;

        for (int i = index; i < trimmed.Length; i++)
        {
            char c = trimmed[i];
            if (c < '0' || c > '9')
                throw new FormatException($"Input string was not in a correct format.");

            hasDigit = true;
            int digit = c - '0';
            result = result * 10 + digit;

            if (negative && -result < int.MinValue || !negative && result > int.MaxValue)
                throw new OverflowException("Value was either too large or too small for an Int32.");
        }

        if (!hasDigit)
            throw new FormatException("Input string was not in a correct format.");

        return negative ? (int)-result : (int)result;
    }
}
