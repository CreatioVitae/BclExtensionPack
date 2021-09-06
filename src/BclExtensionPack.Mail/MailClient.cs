using MailKit.Net.Smtp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BclExtensionPack.Mail {
    public class MailClient : IDisposable {
        readonly SmtpClient _smtpClient;

        public static MailClient Create(Configuration configuration) =>
            new(CreateConnectedAndAuthenticatedSmtpClient(configuration));

        public static async Task<MailClient> CreateAsync(Configuration configuration) =>
            new(await CreateConnectedAndAuthenticatedSmtpClientAsync(configuration));

        private static SmtpClient CreateConnectedAndAuthenticatedSmtpClient(Configuration configuration) =>
            new SmtpClient { Timeout = configuration.TimeoutInMilliseconds }.ConnectAndAuthenticate(configuration);

        private static async Task<SmtpClient> CreateConnectedAndAuthenticatedSmtpClientAsync(Configuration configuration) =>
            await new SmtpClient { Timeout = configuration.TimeoutInMilliseconds }.ConnectAndAuthenticateAsync(configuration);

        public MailClient(SmtpClient smtpClient) =>
            _smtpClient = smtpClient;

        public Task SendAsync(MailMessage mailMessage) =>
            _smtpClient.SendAsync(CreateMimeMessage(mailMessage));

        public static async Task SendAsync(Configuration configuration, MailMessage mailMessage) {
            using var smtpClient = await CreateConnectedAndAuthenticatedSmtpClientAsync(configuration);

            await smtpClient.SendAsync(CreateMimeMessage(mailMessage));
            await smtpClient.DisconnectAsync(true);
        }

        public void Send(MailMessage mailMessage) =>
            _smtpClient.Send(CreateMimeMessage(mailMessage));

        public static void Send(Configuration configuration, MailMessage mailMessage) {
            using var smtpClient = CreateConnectedAndAuthenticatedSmtpClient(configuration);
            smtpClient.Send(CreateMimeMessage(mailMessage));
            smtpClient.Disconnect(true);
        }

        static MimeKit.MimeMessage CreateMimeMessage(MailMessage mailMessage) {
            var message = new MimeKit.MimeMessage();

            message.SetSubject(mailMessage);
            message.Body = mailMessage.Body;
            message.From.Add(mailMessage.From);
            message.To.AddRange(mailMessage.To);

            if (mailMessage?.Cc is not null && mailMessage.Cc.Any()) {
                message.Cc.AddRange(mailMessage.Cc);
            }

            if (mailMessage?.Bcc is not null && mailMessage.Bcc.Any()) {
                message.Bcc.AddRange(mailMessage.Bcc);
            }

            return message;
        }

        public void Dispose() {
            if (_smtpClient == null) {
                return;
            }

            if (_smtpClient.IsConnected) {
                _smtpClient.Disconnect(true);
            }

            _smtpClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    internal static class MimeMessageExtensionLibrary {
        internal static void SetSubject(this MimeKit.MimeMessage mimeMessage, MailMessage mailMessage) =>
            mimeMessage.Headers.Replace(MimeKit.HeaderId.Subject, mailMessage.Encoding, mailMessage.Subject);
    }
}
