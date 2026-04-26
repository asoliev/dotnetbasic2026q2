namespace GreetingLibrary;

public class GreetingService
{
    public static string GetGreeting(string username) => $"{DateTime.Now:HH:mm:ss} Hello, {username}!";
}
