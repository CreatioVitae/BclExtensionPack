using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Resolvers;
using System.Xml.Serialization;

// ReSharper disable once CheckNamespace
namespace System.Net.Http;

static file class XmlSerializer<T> {
    internal static readonly XmlSerializer Instance = new(typeof(T));
}

public static class HttpContentExtensions {
    public static async ValueTask<T> GetAsync<T>(this HttpContent content, IJsonFormatterResolver? resolver = null, CancellationToken cancellationToken = default) {
        resolver ??= StandardResolver.ExcludeNullCamelCase;

        var getResult = await JsonSerializer.DeserializeAsync<T>(await content.ReadAsStreamAsync(cancellationToken), resolver.GetJsonSerializerOptions(), cancellationToken);

        ArgumentNullException.ThrowIfNull(getResult);

        return getResult;
    }

    public static HttpContent CreateJsonHttpContent<T>(this T value, IJsonFormatterResolver? resolver = null) where T : class {
        resolver ??= StandardResolver.ExcludeNullCamelCase;

        var byteArray = JsonSerializer.SerializeToUtf8Bytes(value, resolver.GetJsonSerializerOptions());

        var content = new ByteArrayContent(byteArray);

        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return content;
    }

    public static async ValueTask<T> GetFromXmlAsync<T>(this HttpContent content) {
        await using var responseStream = await content.ReadAsStreamAsync();

        return XmlSerializer<T>.Instance.Deserialize(responseStream) is not object deserialized
            ? throw new FormatException($"{nameof(content)}の形式が{nameof(T)}と異なるため、デシリアライズ出来ません。")
            : (T)deserialized;
    }

    const HttpCompletionOption DefaultCompletionOption = HttpCompletionOption.ResponseContentRead;

    static Uri? CreateUri(string? uri) =>
        uri.IsNullOrWhiteSpace()
            ? null
            : (new(uri, UriKind.RelativeOrAbsolute));

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, string? requestUri) =>
        httpClient.HeadAsync(CreateUri(requestUri));

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, Uri? requestUri) =>
        httpClient.HeadAsync(requestUri, DefaultCompletionOption);

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, string? requestUri, HttpCompletionOption completionOption) =>
        httpClient.HeadAsync(CreateUri(requestUri), completionOption);

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, Uri? requestUri, HttpCompletionOption completionOption) =>
        httpClient.HeadAsync(requestUri, completionOption, CancellationToken.None);

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, string? requestUri, CancellationToken cancellationToken) =>
        httpClient.HeadAsync(CreateUri(requestUri), cancellationToken);

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, Uri? requestUri, CancellationToken cancellationToken) =>
        httpClient.HeadAsync(requestUri, DefaultCompletionOption, cancellationToken);

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, string? requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken) =>
        httpClient.HeadAsync(CreateUri(requestUri), completionOption, cancellationToken);

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, Uri? requestUri, HttpCompletionOption completionOption,
        CancellationToken cancellationToken) =>
        httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, requestUri), completionOption, cancellationToken);

    public static string? GetMediaType(this HttpContent content) =>
        content.Headers.ContentType?.MediaType;
}
