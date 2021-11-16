namespace System.Collections.Generic;
public static class ListExtensions {
    public static List<T> LoopAndAdd<T>(this List<T> collection, T item, int loopCount) {
        for (var i = 1; i <= loopCount; i++) {
            collection.Add(item);
        }

        return collection;
    }
}
