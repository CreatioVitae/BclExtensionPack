// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting;

public static class DefaultConsoleEnvironment {
    public static IDefaultEnvironmentAccessor ConsoleApps { get; } = new ConsoleDefaultEnvironmentAccessor();
}

public class ConsoleDefaultEnvironmentAccessor : IDefaultEnvironmentAccessor {
    public string GetEnvironmentName() =>
        Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? DefaultEnvironmentNames.Production;

    public bool IsDevelopment() =>
        GetEnvironmentName() is DefaultEnvironmentNames.Development;

    public bool IsNotDevelopment() =>
        IsDevelopment() is false;

    public bool IsDevelopmentRemote() =>
        GetEnvironmentName() is DefaultEnvironmentNames.DevelopmentRemote;

    public bool IsNotDevelopmentRemote() =>
        IsDevelopmentRemote() is false;

    public bool IsStaging() =>
        GetEnvironmentName() is DefaultEnvironmentNames.Staging;

    public bool IsNotStaging() =>
        IsStaging() is false;

    public bool IsProduction() =>
        GetEnvironmentName() is DefaultEnvironmentNames.Production;

    public bool IsNotProduction() =>
        IsProduction() is false;
}
