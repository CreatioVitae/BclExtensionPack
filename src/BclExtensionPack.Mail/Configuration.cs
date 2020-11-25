namespace BclExtensionPack.Mail {
    public class Configuration {
        internal string Host { get; }

        internal int Port { get; }

        internal int TimeoutInMilliseconds { get; }

        internal Credential Credential { get; }

        public Configuration(string host, int port, int timeoutInMilliseconds, string userName, string password) {
            Host = host;
            Port = port;
            TimeoutInMilliseconds = timeoutInMilliseconds;
            Credential = new Credential(userName, password);
        }
    }
}
