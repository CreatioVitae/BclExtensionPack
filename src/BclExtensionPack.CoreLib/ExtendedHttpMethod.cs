// ReSharper disable once CheckNamespace
namespace System.Net.Http;
public static class ExtendedHttpMethod {
    public const string PurgeMethodName = "PURGE";

    public static HttpMethod Purge { get; } = new HttpMethod(PurgeMethodName);
}
