using System.Globalization;
using System.Reflection;

namespace Task1;

public abstract class ConfigurationComponentBase
{
    public void LoadSettings()
    {
        foreach (PropertyInfo property in GetConfiguredProperties())
        {
            ConfigurationItemAttribute attribute = property.GetCustomAttribute<ConfigurationItemAttribute>()!;
            IConfigurationProvider provider = GetProvider(attribute.ProviderType);
            string? rawValue = provider.GetValue(attribute.SettingName);

            if (rawValue is null)
                continue;

            property.SetValue(this, ConvertFromString(rawValue, property.PropertyType));
        }
    }

    public void SaveSettings()
    {
        foreach (PropertyInfo property in GetConfiguredProperties())
        {
            ConfigurationItemAttribute attribute = property.GetCustomAttribute<ConfigurationItemAttribute>()!;
            IConfigurationProvider provider = GetProvider(attribute.ProviderType);
            object? value = property.GetValue(this);

            if (value is null)
                continue;

            provider.SetValue(attribute.SettingName, ConvertToString(value));
        }
    }

    protected abstract IConfigurationProvider GetProvider(ConfigurationProviderType providerType);

    private IEnumerable<PropertyInfo> GetConfiguredProperties()
    {
        return GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(property => property.GetCustomAttribute<ConfigurationItemAttribute>() is not null);
    }

    private static object ConvertFromString(string value, Type targetType)
    {
        if (targetType == typeof(string))
            return value;

        if (targetType == typeof(int))
            return int.Parse(value, CultureInfo.InvariantCulture);

        if (targetType == typeof(float))
            return float.Parse(value, CultureInfo.InvariantCulture);

        if (targetType == typeof(TimeSpan))
            return TimeSpan.Parse(value, CultureInfo.InvariantCulture);

        throw new NotSupportedException($"Type '{targetType.Name}' is not supported by configuration attributes.");
    }

    private static string ConvertToString(object value)
    {
        return value switch
        {
            string text => text,
            int number => number.ToString(CultureInfo.InvariantCulture),
            float number => number.ToString(CultureInfo.InvariantCulture),
            TimeSpan timeSpan => timeSpan.ToString("c", CultureInfo.InvariantCulture),
            _ => throw new NotSupportedException($"Type '{value.GetType().Name}' is not supported by configuration attributes."),
        };
    }
}

public enum ConfigurationProviderType
{
    File,
    ConfigurationManager,
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ConfigurationItemAttribute(string settingName, ConfigurationProviderType providerType) : Attribute
{

    public string SettingName { get; } = settingName;

    public ConfigurationProviderType ProviderType { get; } = providerType;
}

public interface IConfigurationProvider
{
    string? GetValue(string settingName);

    void SetValue(string settingName, string value);
}
