using Task1;

var settings = new DemoConfiguration
{
    ApplicationName = "Reflection Configuration Demo",
    RetryCount = 3,
    PollInterval = TimeSpan.FromSeconds(15),
    ScaleFactor = 1.25f,
};

settings.SaveSettings();

var reloadedSettings = new DemoConfiguration();
reloadedSettings.LoadSettings();

Console.WriteLine("Settings were saved and loaded through custom attributes.");
Console.WriteLine(reloadedSettings);
