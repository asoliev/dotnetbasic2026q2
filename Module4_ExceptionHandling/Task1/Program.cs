using System;

namespace Task1;

internal static class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            string input = Console.ReadLine();

            if (input is null) break;

            try
            {
                PrintFirstCharacter(input);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private static void PrintFirstCharacter(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input cannot be an empty string.");

        Console.WriteLine(input[0]);
    }
}
