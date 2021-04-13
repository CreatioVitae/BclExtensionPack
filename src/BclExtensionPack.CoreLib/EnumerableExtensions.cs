using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq {
    public static class EnumerableExtensions {
        public static List<T> AsList<T>(this IEnumerable<T> source) where T : class =>
            (source is List<T> list) ? list : source.ToList();

        [Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1068:CancellationToken パラメーターは最後に指定する必要があります", Justification = "<保留中>")]
        public static async Task ForEachAsync<T>(this IEnumerable<T>? sources, Func<T, Task>? func, int concurrency, CancellationToken cancellationToken = default, bool configureAwait = false) {
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

                    if (t.IsFaulted && t.Exception is not null) {
                        Interlocked.Increment(ref exceptionCount);
                        throw t.Exception;
                    }
                });

                tasks.Add(task);
            }

            await Task.WhenAll(tasks.ToArray()).ConfigureAwait(configureAwait);
        }

        public static bool IsNotNullAndAny<T>(this IEnumerable<T> source) where T : class =>
            source is not null && source.Any();
    }
}
