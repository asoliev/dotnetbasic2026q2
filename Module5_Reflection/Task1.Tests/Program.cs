using Task1;

string baseDirectory = AppContext.BaseDirectory;
string fileSettingsPath = Path.Combine(baseDirectory, "task1.settings");
string jsonSettingsPath = Path.Combine(baseDirectory, "appsettings.json");

try
{
    RunFileProviderTests(fileSettingsPath);
    RunJsonProviderTests(jsonSettingsPath);
    RunBaseClassRoundtripTests(fileSettingsPath, jsonSettingsPath);

    Console.WriteLine("All Task1 tests passed.");
}
finally
{
    if (File.Exists(fileSettingsPath))
        File.Delete(fileSettingsPath);

    if (File.Exists(jsonSettingsPath))
        File.Delete(jsonSettingsPath);
}

void RunFileProviderTests(string settingsPath)
{
    File.Delete(settingsPath);

    var settings = new DemoConfiguration
    {
        RetryCount = 7,
        PollInterval = TimeSpan.FromMinutes(2),
    };

    settings.SaveSettings();

    var loadedSettings = new DemoConfiguration();
    loadedSettings.LoadSettings();

    AssertEqual(7, loadedSettings.RetryCount, "File provider should load the stored integer value.");
    AssertEqual(TimeSpan.FromMinutes(2), loadedSettings.PollInterval, "File provider should load the stored TimeSpan value.");
}

void RunJsonProviderTests(string settingsPath)
{
    File.Delete(settingsPath);

    var settings = new DemoConfiguration
    {
        ApplicationName = "Reflection Demo",
        ScaleFactor = 1.5f,
    };

    settings.SaveSettings();

    var loadedSettings = new DemoConfiguration();
    loadedSettings.LoadSettings();

    AssertEqual("Reflection Demo", loadedSettings.ApplicationName, "JSON provider should load the stored string value.");
    AssertEqual(1.5f, loadedSettings.ScaleFactor, "JSON provider should load the stored float value.");
}

void RunBaseClassRoundtripTests(string roundtripFileSettingsPath, string roundtripJsonSettingsPath)
{
    File.Delete(roundtripFileSettingsPath);
    File.Delete(roundtripJsonSettingsPath);

    var writer = new DemoConfiguration
    {
        RetryCount = 11,
        PollInterval = TimeSpan.FromSeconds(45),
        ApplicationName = "Roundtrip",
        ScaleFactor = 2.75f
    };
    writer.SaveSettings();

    var reader = new DemoConfiguration();
    reader.LoadSettings();

    AssertEqual(11, reader.RetryCount, "Base class should load integer properties.");
    AssertEqual(TimeSpan.FromSeconds(45), reader.PollInterval, "Base class should load TimeSpan properties.");
    AssertEqual("Roundtrip", reader.ApplicationName, "Base class should load string properties.");
    AssertEqual(2.75f, reader.ScaleFactor, "Base class should load float properties.");
}

static void AssertEqual<T>(T expected, T actual, string message)
{
    if (!Equals(expected, actual))
        throw new InvalidOperationException($"{message} Expected: {expected}, Actual: {actual}");
}
