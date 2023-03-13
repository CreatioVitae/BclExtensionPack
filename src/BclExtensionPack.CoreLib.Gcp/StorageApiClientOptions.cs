// ReSharper disable once CheckNamespace
namespace System.IO.Gcs;
public record StorageApiClientOptions {
    public required string BaseUri { get; init; }

    public required string BucketName { get; init; }

    public string VirtualPath { get; init; } = string.Empty;

    public required bool UseTestService { get; init; }
}
