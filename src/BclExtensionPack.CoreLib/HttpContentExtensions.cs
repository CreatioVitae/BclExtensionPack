using System.Net.Http.Headers;
using System.Xml.Serialization;
using Utf8Json;
using Utf8Json.Resolvers;

namespace System.Net.Http;
public static class HttpContentExtensions {
    public static async ValueTask<T> GetAsync<T>(this HttpContent content, IJsonFormatterResolver? resolver = null) {
        resolver ??= StandardResolver.Default;

        return JsonSerializer.Deserialize<T>(await content.ReadAsByteArrayAsync(), resolver);
    }

    public static HttpContent CreateJsonHttpContent<T>(this T value, IJsonFormatterResolver? resolver = null) where T : class {
        resolver ??= StandardResolver.Default;

        var byteArray = JsonSerializer.Serialize(value, resolver);

        var content = new ByteArrayContent(byteArray);

        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return content;
    }

    public static async ValueTask<T> GetFromXmlAsync<T>(this HttpContent content) {
        await using var responseStream = await content.ReadAsStreamAsync();

        return new XmlSerializer(typeof(T)).Deserialize(responseStream) is not object deserialized
            ? throw new FormatException($"{nameof(content)}の形式が{nameof(T)}と異なるため、デシリアライズ出来ません。")
            : (T)deserialized;
    }

    const HttpCompletionOption defaultCompletionOption = HttpCompletionOption.ResponseContentRead;

    static Uri? CreateUri(string? uri) =>
        uri.IsNullOrWhiteSpace()
            ? null
            : (new(uri, UriKind.RelativeOrAbsolute));

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, string? requestUri) =>
        httpClient.HeadAsync(CreateUri(requestUri));

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, Uri? requestUri) =>
        httpClient.HeadAsync(requestUri, defaultCompletionOption);

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, string? requestUri, HttpCompletionOption completionOption) =>
        httpClient.HeadAsync(CreateUri(requestUri), completionOption);

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, Uri? requestUri, HttpCompletionOption completionOption) =>
        httpClient.HeadAsync(requestUri, completionOption, CancellationToken.None);

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, string? requestUri, CancellationToken cancellationToken) =>
        httpClient.HeadAsync(CreateUri(requestUri), cancellationToken);

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, Uri? requestUri, CancellationToken cancellationToken) =>
        httpClient.HeadAsync(requestUri, defaultCompletionOption, cancellationToken);

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, string? requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken) =>
        httpClient.HeadAsync(CreateUri(requestUri), completionOption, cancellationToken);

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, Uri? requestUri, HttpCompletionOption completionOption,
        CancellationToken cancellationToken) =>
        httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, requestUri), completionOption, cancellationToken);

    public static string? GetMediaType(this HttpContent content) =>
        content.Headers.ContentType?.MediaType;
}
