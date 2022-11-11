using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting;
public static class DefaultEnvironmentNames {
    public const string Development = nameof(Development);

    public const string DevelopmentRemote = nameof(DevelopmentRemote);

    public const string Staging = nameof(Staging);

    public const string Production = nameof(Production);
}

public interface IDefaultEnvironmentAccessor {
    public string GetEnvironmentName();

    public bool IsDevelopment();

    public bool IsNotDevelopment();

    public bool IsDevelopmentRemote();

    public bool IsNotDevelopmentRemote();

    public bool IsStaging();

    public bool IsNotStaging();

    public bool IsProduction();

    public bool IsNotProduction();
}

public static class DefaultEnvironmentAccessorExtensions {
    public static IConfiguration CreateConfiguration(this IDefaultEnvironmentAccessor defaultEnvironment) =>
        new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{defaultEnvironment.GetEnvironmentName()}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
}
