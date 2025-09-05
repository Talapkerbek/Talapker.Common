namespace Talapker.Common.Settings;

public class EFCoreSettings
{
    public string Host { get; init; } = String.Empty;
    public int Port { get; init; }
    public string UserName { get; init; } = String.Empty;
    public string Password { get; init; } = String.Empty;
    public string Database { get; init; } = String.Empty;

    public string ConnectionString => $"Host={Host};Port={Port};Username={UserName};Password={Password};Database={Database}";
}