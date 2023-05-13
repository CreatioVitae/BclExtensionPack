namespace System.Linq;

public static class EnumerableExtensions {
    public static List<T> AsList<T>(this IEnumerable<T> source) =>
        (source is List<T> list) ? list : source.ToList();

    public static List<T>? AsListOrDefault<T>(this IEnumerable<T>? source) =>
        (source is List<T> list) ? list : source?.ToList();

    public static async Task ForEachAsync<T>(this IEnumerable<T>? sources, Func<T, Task>? func, int concurrency, bool configureAwait = false, CancellationToken cancellationToken = default) {
        if (sources is null || !sources.Any()) { throw new ArgumentNullException(nameof(sources)); }

        if (func is null) { throw new ArgumentNullException(nameof(func)); }

        if (concurrency <= 0) { throw new ArgumentOutOfRangeException($"{nameof(concurrency)}は1以上、{int.MaxValue}以下に設定してください。但し、同時並列数は現実的な範囲内で設定することをお勧めします。"); }

        using var semaphore = new SemaphoreSlim(initialCount: concurrency, maxCount: concurrency);
        var exceptionCount = 0;
        var tasks = new List<Task>();

        foreach (var item in sources) {
            if (exceptionCount > 0) { break; }

            cancellationToken.ThrowIfCancellationRequested();

            await semaphore.WaitAsync(cancellationToken).ConfigureAwait(configureAwait);

            var task = func.Invoke(item).ContinueWith(t => {
                semaphore.Release();

                if (t is { IsFaulted: true, Exception: { } }) {
                    Interlocked.Increment(ref exceptionCount);
                    throw t.Exception;
                }
            });

            tasks.Add(task);
        }

        await Task.WhenAll(tasks.ToArray()).ConfigureAwait(configureAwait);
    }

    public static bool IsAny<T>([NotNullWhen(true)] this IEnumerable<T>? source) =>
        source is not null && source.Any();

    public static bool IsAny<T>([NotNullWhen(true)] this IEnumerable<T>? source, Func<T, bool> predicate) =>
        source is not null && source.Any(predicate);

    public static TSource? FirstOrDefault<TSource, TState>(this IEnumerable<TSource> source, Func<TSource, TState, bool> predicate, TState state) {
        if (source is null) {
            throw new ArgumentNullException(nameof(source));
        }

        if (predicate is null) {
            throw new ArgumentNullException(nameof(predicate));
        }

        foreach (var element in source) {
            if (predicate(element, state)) {
                return element;
            }
        }

        return default;
    }

    public static IEnumerable<T> WhereNonNull<T>(this IEnumerable<T?> source) =>
        source.OfType<T>();
}
