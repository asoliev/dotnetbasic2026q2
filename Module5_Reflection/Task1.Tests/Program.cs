using Task1;
using System.Globalization;

string tempDirectory = Path.Combine(Path.GetTempPath(), "Module5_Reflection_Task1_Tests", Guid.NewGuid().ToString("N"));
Directory.CreateDirectory(tempDirectory);

try
{
    RunFileProviderTests(tempDirectory);
    RunJsonProviderTests(tempDirectory);
    RunBaseClassRoundtripTests(tempDirectory);

    Console.WriteLine("All Task1 tests passed.");
    return;
}
finally
{
    if (Directory.Exists(tempDirectory))
        Directory.Delete(tempDirectory, recursive: true);
}

void RunFileProviderTests(string tempDirectoryPath)
{
    string filePath = Path.Combine(tempDirectoryPath, "settings.config");
    var provider = new FileConfigurationProvider(filePath);

    provider.SetValue("RetryCount", 7.ToString(CultureInfo.InvariantCulture));
    provider.SetValue("PollInterval", TimeSpan.FromMinutes(2).ToString("c", CultureInfo.InvariantCulture));

    AssertEqual("7", provider.GetValue("RetryCount"), "File provider should return the stored integer value.");
    AssertEqual("00:02:00", provider.GetValue("PollInterval"), "File provider should return the stored TimeSpan value.");
}

void RunJsonProviderTests(string tempDirectoryPath)
{
    string filePath = Path.Combine(tempDirectoryPath, "appsettings.json");
    var provider = new ConfigurationManagerConfigurationProvider(filePath);

    provider.SetValue("ApplicationName", "Reflection Demo");
    provider.SetValue("ScaleFactor", 1.5f.ToString(CultureInfo.InvariantCulture));

    AssertEqual("Reflection Demo", provider.GetValue("ApplicationName"), "JSON provider should return the stored string value.");
    AssertEqual("1.5", provider.GetValue("ScaleFactor"), "JSON provider should return the stored float value.");
}

void RunBaseClassRoundtripTests(string tempDirectoryPath)
{
    string filePath = Path.Combine(tempDirectoryPath, "settings.config");
    string jsonPath = Path.Combine(tempDirectoryPath, "appsettings.json");

    var writer = new TestConfigurationComponent(
        new FileConfigurationProvider(filePath),
        new ConfigurationManagerConfigurationProvider(jsonPath))
    {
        RetryCount = 11,
        PollInterval = TimeSpan.FromSeconds(45),
        ApplicationName = "Roundtrip",
        ScaleFactor = 2.75f
    };
    writer.SaveSettings();

    var reader = new TestConfigurationComponent(
        new FileConfigurationProvider(filePath),
        new ConfigurationManagerConfigurationProvider(jsonPath));
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

internal sealed class TestConfigurationComponent(FileConfigurationProvider fileProvider, ConfigurationManagerConfigurationProvider jsonProvider) : ConfigurationComponentBase
{
    [ConfigurationItem("RetryCount", ConfigurationProviderType.File)]
    public int RetryCount { get; set; }

    [ConfigurationItem("PollInterval", ConfigurationProviderType.File)]
    public TimeSpan PollInterval { get; set; }

    [ConfigurationItem("ApplicationName", ConfigurationProviderType.ConfigurationManager)]
    public string ApplicationName { get; set; } = string.Empty;

    [ConfigurationItem("ScaleFactor", ConfigurationProviderType.ConfigurationManager)]
    public float ScaleFactor { get; set; }

    protected override IConfigurationProvider GetProvider(ConfigurationProviderType providerType)
    {
        return providerType switch
        {
            ConfigurationProviderType.File => fileProvider,
            ConfigurationProviderType.ConfigurationManager => jsonProvider,
            _ => throw new ArgumentOutOfRangeException(nameof(providerType), providerType, null),
        };
    }
}