using Cysharp.Text;

namespace System;
public static class StringExtensions {
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? s) =>
        string.IsNullOrWhiteSpace(s);

    public static bool IsNotNullAndNotWhiteSpace([NotNullWhen(true)] this string? s) =>
        !string.IsNullOrWhiteSpace(s);

    public static string Remove(this string? s, string keyword) {
        ArgumentNullException.ThrowIfNull(s);

        if (s.IndexOf(keyword) is -1) {
            return s;
        }

        using var stringBuilder = ZString.CreateStringBuilder();
        stringBuilder.Append(s);

        stringBuilder.Replace(keyword, string.Empty);

        return stringBuilder.ToString();
    }
}
