using MimeKit;
using MimeKit.Text;
using System.IO;
using System.Text;
using System.Threading;

namespace BclExtensionPack.Mail;

public class MailMessage {

    internal MailboxAddress From { get; }

    internal IEnumerable<MailboxAddress> To { get; }

    internal IEnumerable<MailboxAddress>? Cc { get; }

    internal IEnumerable<MailboxAddress>? Bcc { get; }

    internal string Subject { get; }

    internal MimeEntity Body { get; }

    internal Encoding Encoding { get; }

    public MailMessage(string subject, (bool isHtml, string text) body, (string? name, string address) from,
        IEnumerable<(string? name, string address)> to,
        IEnumerable<(string? name, string address)>? cc = default,
        IEnumerable<(string? name, string address)>? bcc = default, Encoding? encoding = default) {

        //Todo:Validation.
        Encoding = encoding ?? Encoding.GetEncoding("iso-2022-jp");
        From = new(Encoding, from.name, from.address);
        To = to.Select(item => new MailboxAddress(Encoding, item.name, item.address));
        Cc = cc?.Select(item => new MailboxAddress(Encoding, item.name, item.address));
        Bcc = bcc?.Select(item => new MailboxAddress(Encoding, item.name, item.address));
        Subject = subject;

        static TextPart CreateMailBody(bool isHtml, string text, Encoding enc) {
            var textPart = new TextPart(isHtml ? TextFormat.Html : TextFormat.Plain);
            textPart.SetText(enc, text);

            return textPart;
        }

        Body = CreateMailBody(body.isHtml, body.text, Encoding);
    }

    MailMessage(string subject, MimeEntity body, MailboxAddress from, IEnumerable<MailboxAddress> to,
        IEnumerable<MailboxAddress>? cc, IEnumerable<MailboxAddress>? bcc, Encoding encoding) {
        Encoding = encoding;
        From = from;
        To = to;
        Cc = cc;
        Bcc = bcc;
        Subject = subject;
        Body = body;
    }

    public static MailMessage CreateMultiPartMailMessage(string subject, (string text, string html) body,
        (string? name, string address) from, IEnumerable<(string? name, string address)> to,
        IEnumerable<(string? name, string address)>? cc = default,
        IEnumerable<(string? name, string address)>? bcc = default, Encoding? encoding = default) {

        static MimeEntity CreateMailBody(string text, string html, Encoding enc) {
            var textPart = new TextPart(TextFormat.Plain);
            textPart.SetText(enc, text);

            var htmlPart = new TextPart(TextFormat.Html);
            htmlPart.SetText(enc, html);

            var multipart = new Multipart("mixed") {
                textPart,
                htmlPart
            };

            return multipart;
        }

        encoding ??= Encoding.GetEncoding("iso-2022-jp");

        return new(
            subject,
            CreateMailBody(body.text, body.html, encoding),
            new(encoding, from.name, from.address),
            to.Select(item => new MailboxAddress(encoding, item.name, item.address)),
            cc?.Select(item => new MailboxAddress(encoding, item.name, item.address)),
            bcc?.Select(item => new MailboxAddress(encoding, item.name, item.address)),
            encoding
        );
    }
}
