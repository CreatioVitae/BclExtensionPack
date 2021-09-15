using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace System {
    public static class Base64StringExtensions {
        public static bool TryDecodeUtf8FromBase64String(this string base64, [NotNullWhen(true)] out string? utf8) {
            Span<byte> bytes = stackalloc byte[256];
            bool result;

            (result, utf8) = Convert.TryFromBase64String(base64, bytes, out var bytesWritten)
                ? (true, Encoding.UTF8.GetString(bytes.Slice(0, bytesWritten)))
                : (false, null);

            return result;
        }

        public static string EncodeBase64String(this string utf8) =>
            Convert.ToBase64String(Encoding.UTF8.GetBytes(utf8));
    }
}
