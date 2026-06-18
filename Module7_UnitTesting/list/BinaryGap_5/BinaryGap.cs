using System;
namespace BinaryGap_5;

public static class BinaryGap
{
    public static int Solution(int number)
    {
        if (number < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(number));
        }

        int longestGap = 0;
        int currentGap = 0;
        bool counting = false;

        while (number > 0)
        {
            if ((number & 1) == 1)
            {
                if (counting && currentGap > longestGap)
                {
                    longestGap = currentGap;
                }

                counting = true;
                currentGap = 0;
            }
            else if (counting)
            {
                currentGap++;
            }

            number >>= 1;
        }

        return longestGap;
    }
}