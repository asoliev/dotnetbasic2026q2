using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Task1.Contracts;

namespace Task1.ConfigurationManagerProviderPlugin;

[ConfigurationProvider(ConfigurationProviderType.ConfigurationManager)]
public sealed class ConfigurationManagerConfigurationProvider : Task1.Contracts.IConfigurationProvider
{
    private readonly string filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
    private readonly ConfigurationManager configurationManager;

    public ConfigurationManagerConfigurationProvider()
    {
        configurationManager = new ConfigurationManager();
        configurationManager.SetBasePath(AppContext.BaseDirectory);
        configurationManager.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
    }

    public string? GetValue(string settingName) => configurationManager[settingName];

    public void SetValue(string settingName, string value)
    {
        configurationManager[settingName] = value;
        SaveValues();
    }

    private void SaveValues()
    {
        Dictionary<string, string> values = configurationManager
            .AsEnumerable()
            .Where(pair => pair.Value is not null)
            .ToDictionary(pair => pair.Key, pair => pair.Value!, StringComparer.OrdinalIgnoreCase);

        string json = JsonSerializer.Serialize(values, new JsonSerializerOptions
        {
            WriteIndented = true,
        });

        File.WriteAllText(filePath, json);
    }
}
