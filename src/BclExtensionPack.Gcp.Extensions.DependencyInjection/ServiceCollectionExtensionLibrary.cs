using Microsoft.Extensions.Configuration;
using System.IO.Gcs;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensionLibrary {
    public static IServiceCollection AddStorageApiClient(this IServiceCollection services, IConfiguration configuration) =>
        services.AddScoped(_ => new StorageApiClient(configuration.GetStorageApiClientOptions()));

    public static StorageApiClientOptions GetStorageApiClientOptions(this IConfiguration configuration) =>
        configuration.GetSection(nameof(StorageApiClientOptions)).GetAvailable<StorageApiClientOptions>();
}
