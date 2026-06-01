using System.Text.Json;

namespace Task1;

public sealed class FileConfigurationProvider(string? filePath = null) : IConfigurationProvider
{
    private readonly string filePath = filePath ?? Path.Combine(AppContext.BaseDirectory, "task1.settings");

    public string? GetValue(string settingName) => LoadValues().TryGetValue(settingName, out string? value) ? value : null;

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

        var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (string line in File.ReadAllLines(filePath))
        {
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith('#'))
                continue;

            KeyValuePair<string, string>? entry = ParseLine(line);
            if (entry is null)
                continue;

            values[entry.Value.Key] = entry.Value.Value;
        }

        return values;
    }

    private void SaveValues(Dictionary<string, string> values)
    {
        IEnumerable<string> lines = values
            .OrderBy(pair => pair.Key, StringComparer.OrdinalIgnoreCase)
            .Select(pair => $"{Uri.EscapeDataString(pair.Key)}={Uri.EscapeDataString(pair.Value)}");

        File.WriteAllLines(filePath, lines);
    }

    private static KeyValuePair<string, string>? ParseLine(string line)
    {
        int separatorIndex = line.IndexOf('=');
        if (separatorIndex < 0)
            return null;

        string key = Uri.UnescapeDataString(line[..separatorIndex]);
        string value = Uri.UnescapeDataString(line[(separatorIndex + 1)..]);

        return new KeyValuePair<string, string>(key, value);
    }
}

public sealed class ConfigurationManagerConfigurationProvider(string? filePath = null) : IConfigurationProvider
{
    private readonly string filePath = filePath ?? Path.Combine(AppContext.BaseDirectory, "appsettings.json");

    public string? GetValue(string settingName) => LoadValues().TryGetValue(settingName, out string? value) ? value : null;

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
