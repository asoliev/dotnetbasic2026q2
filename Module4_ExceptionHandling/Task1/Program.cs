using System;

namespace Task1;

internal static class Program
{
    private static int Main()
    {
        Console.CancelKeyPress += OnCancelKeyPress;

        try
        {
            ProcessInputLines();
            return 0;
        }
        finally
        {
            Console.CancelKeyPress -= OnCancelKeyPress;
        }
    }

    private static void ProcessInputLines()
    {
        string input;
        while ((input = Console.ReadLine()) is not null)
        {
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

    private static void OnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
    {
        e.Cancel = true;
        Environment.Exit(0);
    }
}
