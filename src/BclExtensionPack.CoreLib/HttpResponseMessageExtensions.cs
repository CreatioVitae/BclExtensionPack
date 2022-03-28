using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace System.Net.Http;
public static class HttpResponseMessageExtensions {
    public static async Task<HttpResponseMessage> IfStatusCodeIsAllowed([Required] this Task<HttpResponseMessage> t, [Required] HttpStatusCode[] allowedHttpStatusCodes) {
        var response = await t;

        return allowedHttpStatusCodes.Contains(response.StatusCode)
            ? response
            : throw response.CreateHttpRequestException();
    }

    public static HttpRequestException CreateHttpRequestException(this HttpResponseMessage response) =>
        (response.StatusCode, response?.RequestMessage?.RequestUri).CreateHttpRequestException();

    public static HttpRequestException CreateHttpRequestException(this (HttpStatusCode statusCode, Uri? requestUri) response) =>
        new($"{nameof(response.statusCode)}:{response.statusCode},{nameof(response.requestUri)}:{response.requestUri?.ToString()}");
}

public static class AllowedHttpStatusCodes {
    public static readonly HttpStatusCode[] Ok = new HttpStatusCode[] { HttpStatusCode.OK };
    public static readonly HttpStatusCode[] OkOrNotFound = new HttpStatusCode[] { HttpStatusCode.OK, HttpStatusCode.NotFound };
}
