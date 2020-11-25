using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace BclExtensionPack.Mail {
    internal static class SmtpClientExtensionLibrary {
        internal static SmtpClient ConnectAndAuthenticate(this SmtpClient smtpClient, Configuration configuration) {
            smtpClient.Connect(configuration.Host, configuration.Port);

            if (configuration.Credential.AreNeedAuthentication) {
                smtpClient.Authenticate(configuration.Credential.UserName, configuration.Credential.Password);
            }

            return smtpClient;
        }

        internal static async Task<SmtpClient> ConnectAndAuthenticateAsync(this SmtpClient smtpClient, Configuration configuration) {
            await smtpClient.ConnectAsync(configuration.Host, configuration.Port).ConfigureAwait(false);

            if (configuration.Credential.AreNeedAuthentication) {
                await smtpClient.AuthenticateAsync(configuration.Credential.UserName, configuration.Credential.Password).ConfigureAwait(false);
            }

            return smtpClient;
        }
    }
}
