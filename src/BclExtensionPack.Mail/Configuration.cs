using MailKit.Security;

namespace BclExtensionPack.Mail;
public class Configuration {
    internal string Host { get; }

    internal int Port { get; }

    internal int TimeoutInMilliseconds { get; }

    internal Credential Credential { get; }

    internal SecureSocketOptions SecureSocketOption { get; }

    public Configuration(string host, int port, int timeoutInMilliseconds, string? userName, string? password, string? secureSocketOption) {
        Host = host;
        Port = port;
        TimeoutInMilliseconds = timeoutInMilliseconds;
        Credential = new Credential(userName, password);

        SecureSocketOption = Enum.TryParse<SecureSocketOptions>(secureSocketOption, out var secureSocketOptionEnum)
            ? secureSocketOptionEnum
            : SecureSocketOptions.StartTlsWhenAvailable;
    }
}
