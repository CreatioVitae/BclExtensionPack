using System.Net;

// ReSharper disable once CheckNamespace
namespace System.IO.Gcs;

public class StorageApiClient : IDisposable {
    public StorageClient ClientInstance { get; }

    StorageApiClientOptions Options { get; }

    public StorageApiClient(StorageApiClientOptions options) {
        ClientInstance = new StorageClientBuilder {
            BaseUri = options.BaseUri,
            UnauthenticatedAccess = options.UseTestService,
            Credential = (options.UseTestService) ? null : GoogleCredential.GetApplicationDefault()
        }.Build();

        Options = options;
    }

    public async ValueTask<(bool result, MemoryStream? stream)> GetStreamAsync(string objectName) {
        var memoryStream = new MemoryStream();

        try {
            await ClientInstance.DownloadObjectAsync(
                Options.BucketName,
                $"{Options.VirtualPath}{objectName}", memoryStream
            );
        }
        catch (GoogleApiException ex) when (ex is { HttpStatusCode: HttpStatusCode.NotFound }) {
            return (false, default);
        }

        memoryStream.Seek(0, SeekOrigin.Begin);

        return (true, memoryStream);
    }

    public void Dispose() =>
        ClientInstance.Dispose();
}
