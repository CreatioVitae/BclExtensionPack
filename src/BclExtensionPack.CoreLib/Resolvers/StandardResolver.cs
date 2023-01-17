using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

// ReSharper disable once CheckNamespace
namespace System.Text.Json.Resolvers;

public static class StandardResolver {
    public static readonly IJsonFormatterResolver ExcludeNullCamelCase = ExcludeNullCamelCaseStandardResolver.Instance;
}

internal sealed class ExcludeNullCamelCaseStandardResolver : IJsonFormatterResolver {
    public static readonly IJsonFormatterResolver Instance = new ExcludeNullCamelCaseStandardResolver();

    static readonly JsonSerializerOptions JsonSerializerOptions = new() {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        Encoder = new NoEscapingJsonEncoder(),
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    ExcludeNullCamelCaseStandardResolver() { }

    public JsonSerializerOptions GetJsonSerializerOptions() =>
        JsonSerializerOptions;
}
