using System.Diagnostics.CodeAnalysis;

namespace System {
    public static class StringExtensions {
        public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? s) =>
            string.IsNullOrWhiteSpace(s);

        public static bool IsNotNullAndNotWhiteSpace([NotNullWhen(true)] this string? s) =>
            !string.IsNullOrWhiteSpace(s);
    }
}
