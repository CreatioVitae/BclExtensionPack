using System.Net.Http.Headers;
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
}
