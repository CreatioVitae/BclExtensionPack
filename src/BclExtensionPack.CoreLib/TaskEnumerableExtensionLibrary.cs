using System.Collections.Generic;

namespace System.Threading.Tasks {
    public static class TaskEnumerableExtensionLibrary {
        public static Task WhenAll(this IEnumerable<Task> tasks) =>
            Task.WhenAll(tasks);

        public static Task<T[]> WhenAll<T>(this IEnumerable<Task<T>> tasks) =>
            Task.WhenAll(tasks);
    }
}
