using Utf8Json;
using Utf8Json.Resolvers;

namespace System.Net.Http; 
public static class HttpContentExtensions {
    public static async ValueTask<T> Get<T>(HttpContent content, IJsonFormatterResolver? resolver = null) {
        resolver ??= StandardResolver.Default;

        return JsonSerializer.Deserialize<T>(await content.ReadAsByteArrayAsync(), resolver);
    }
}
