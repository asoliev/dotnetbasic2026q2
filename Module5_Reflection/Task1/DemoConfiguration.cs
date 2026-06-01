namespace Task1;

public sealed class DemoConfiguration : ConfigurationComponentBase
{
    private readonly FileConfigurationProvider fileConfigurationProvider = new();
    private readonly ConfigurationManagerConfigurationProvider configurationManagerConfigurationProvider = new();

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
            ConfigurationProviderType.File => fileConfigurationProvider,
            ConfigurationProviderType.ConfigurationManager => configurationManagerConfigurationProvider,
            _ => throw new ArgumentOutOfRangeException(nameof(providerType), providerType, null),
        };
    }

    public override string ToString() => $"ApplicationName = {ApplicationName}, RetryCount = {RetryCount}, PollInterval = {PollInterval}, ScaleFactor = {ScaleFactor}";
}
