// See https://aka.ms/new-console-template for more information
using GreetingLibrary;

if (args.Length == 0)
{
    Console.WriteLine("Please provide a username as a command line argument.");
    return;
}

string username = args[0];
Console.WriteLine(GreetingService.GetGreeting(username));
