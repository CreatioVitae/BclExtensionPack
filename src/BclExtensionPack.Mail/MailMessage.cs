using MimeKit;
using MimeKit.Text;
using System.Text;

namespace BclExtensionPack.Mail;
public class MailMessage {

    internal MailboxAddress From { get; }

    internal IEnumerable<MailboxAddress> To { get; }

    internal IEnumerable<MailboxAddress>? Cc { get; }

    internal IEnumerable<MailboxAddress>? Bcc { get; }

    internal string Subject { get; }

    internal TextPart Body { get; }

    internal Encoding Encoding { get; }

    public MailMessage(string subject, (bool isHtml, string text) body, (string name, string address) from, IEnumerable<(string name, string address)> to,
        IEnumerable<(string name, string address)>? cc = default, IEnumerable<(string name, string address)>? bcc = default, Encoding? encoding = default) {

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
}
