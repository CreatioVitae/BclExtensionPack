using System.Collections.Generic;

namespace System.Linq {
    public static class EnumerableExtensions {
        public static List<T> AsList<T>(this IEnumerable<T> source) where T : class =>
            (source is List<T> list) ? list : source.ToList();
    }
}
