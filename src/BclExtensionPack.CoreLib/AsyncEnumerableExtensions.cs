// ReSharper disable once CheckNamespace
namespace System.Linq;

public static class AsyncEnumerableExtensions {
    public static async IAsyncEnumerable<TDist> ToLoadAsync<TSource, TDist>(this IAsyncEnumerable<TSource> pubicPropsAsyncEnumerable, Func<TSource, TDist> createApiResultMethod) {
        await foreach (var publicProps in pubicPropsAsyncEnumerable) {
            yield return createApiResultMethod(publicProps);
        }
    }

    public static async ValueTask<IEnumerable<TDist>> ToEnumerableAsync<TSource, TDist>(this IAsyncEnumerable<TSource> pubicPropsAsyncEnumerable, Func<TSource, TDist> createApiResultMethod) =>
        await pubicPropsAsyncEnumerable.ToLoadAsync(createApiResultMethod).ToListAsync();
}
