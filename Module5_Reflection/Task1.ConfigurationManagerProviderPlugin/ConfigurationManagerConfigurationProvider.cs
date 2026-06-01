using System.Text.Json;
using Task1.Contracts;

namespace Task1.ConfigurationManagerProviderPlugin;

[ConfigurationProvider(ConfigurationProviderType.ConfigurationManager)]
public sealed class ConfigurationManagerConfigurationProvider : IConfigurationProvider
{
    private readonly string filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

    public string? GetValue(string settingName) => LoadValues().GetValueOrDefault(settingName);

    public void SetValue(string settingName, string value)
    {
        Dictionary<string, string> values = LoadValues();
        values[settingName] = value;
        SaveValues(values);
    }

    private Dictionary<string, string> LoadValues()
    {
        if (!File.Exists(filePath))
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        using FileStream stream = File.OpenRead(filePath);
        using var document = JsonDocument.Parse(stream);

        if (document.RootElement.ValueKind != JsonValueKind.Object)
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (JsonProperty property in document.RootElement.EnumerateObject())
        {
            values[property.Name] = property.Value.ValueKind switch
            {
                JsonValueKind.String => property.Value.GetString() ?? string.Empty,
                JsonValueKind.Number => property.Value.ToString(),
                JsonValueKind.True => bool.TrueString,
                JsonValueKind.False => bool.FalseString,
                JsonValueKind.Null => string.Empty,
                _ => property.Value.ToString(),
            };
        }

        return values;
    }

    private void SaveValues(Dictionary<string, string> values)
    {
        string json = JsonSerializer.Serialize(values, new JsonSerializerOptions
        {
            WriteIndented = true,
        });

        File.WriteAllText(filePath, json);
    }
}
