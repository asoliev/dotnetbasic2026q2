using Task1.Contracts;

namespace Task1;

public sealed class DemoConfiguration : ConfigurationComponentBase
{
    [ConfigurationItem("RetryCount", ConfigurationProviderType.File)]
    public int RetryCount { get; set; }

    [ConfigurationItem("PollInterval", ConfigurationProviderType.File)]
    public TimeSpan PollInterval { get; set; }

    [ConfigurationItem("ApplicationName", ConfigurationProviderType.ConfigurationManager)]
    public string ApplicationName { get; set; } = string.Empty;

    [ConfigurationItem("ScaleFactor", ConfigurationProviderType.ConfigurationManager)]
    public float ScaleFactor { get; set; }

    public override string ToString() => $"ApplicationName = {ApplicationName}, RetryCount = {RetryCount}, PollInterval = {PollInterval}, ScaleFactor = {ScaleFactor}";
}
