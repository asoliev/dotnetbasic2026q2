namespace Task1.Contracts;

public enum ConfigurationProviderType
{
    File,
    ConfigurationManager,
}

public interface IConfigurationProvider
{
    string? GetValue(string settingName);

    void SetValue(string settingName, string value);
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class ConfigurationProviderAttribute(ConfigurationProviderType providerType) : Attribute
{
    public ConfigurationProviderType ProviderType { get; } = providerType;
}