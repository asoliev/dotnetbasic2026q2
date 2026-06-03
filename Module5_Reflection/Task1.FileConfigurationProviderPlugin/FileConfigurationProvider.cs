using Task1.Contracts;

namespace Task1.FileConfigurationProviderPlugin;

[ConfigurationProvider(ConfigurationProviderType.File)]
public sealed class FileConfigurationProvider : IConfigurationProvider
{
    private readonly string filePath = Path.Combine(AppContext.BaseDirectory, "task1.settings");

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
