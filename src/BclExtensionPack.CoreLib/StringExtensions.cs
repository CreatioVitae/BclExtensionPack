using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace System {
    public static class StringExtensions {
        public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? s) =>
            string.IsNullOrWhiteSpace(s);

        public static bool IsNotNullAndNotWhiteSpace([NotNullWhen(true)] this string? s) =>
            !string.IsNullOrWhiteSpace(s);

        public static bool TryDecodeUtf8FromBase64String(this string base64, out string? utf8) {
            Span<byte> bytes = stackalloc byte[256];
            bool result;

            (result, utf8) = Convert.TryFromBase64String(base64, bytes, out var bytesWritten)
                ? (true, Encoding.UTF8.GetString(bytes.Slice(0, bytesWritten)))
                : (false, null);

            return result;
        }
    }
}
