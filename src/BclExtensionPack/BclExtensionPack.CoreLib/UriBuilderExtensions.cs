using System;

namespace BclExtensionPack.CoreLib {
    public static class UriBuilderExtensions {
        public static UriBuilder SetPort(this UriBuilder uriBuilder, int port) {
            if (port < 1) {
                throw new ArgumentOutOfRangeException(nameof(port));
            }

            uriBuilder.Port = port;
            return uriBuilder;
        }

        public static UriBuilder UnsetPort(this UriBuilder uriBuilder) {
            uriBuilder.Port = -1;
            return uriBuilder;
        }

        public static UriBuilder AppendPath(this UriBuilder uriBuilder, string path) {
            if (string.IsNullOrWhiteSpace(path)) {
                throw new ArgumentNullException(nameof(path));
            }

            const char pathCombineChar = '/';

            uriBuilder.Path = $"{uriBuilder.Path.TrimEnd(pathCombineChar)}{pathCombineChar}{path.TrimStart(pathCombineChar)}";
            return uriBuilder;
        }

        public static UriBuilder UseScheme(this UriBuilder uriBuilder, string scheme) {
            if (string.IsNullOrWhiteSpace(scheme)) {
                throw new ArgumentNullException(nameof(scheme));
            }

            uriBuilder.Scheme = scheme;
            return uriBuilder;
        }
    }
}
