using BclExtensionPack.Mail.Tests.TestContexts;
using Microsoft.Extensions.DependencyInjection;

namespace BclExtensionPack.Mail.Tests;

public class MailSendTest : IDisposable, IClassFixture<TestContext> {

    TestContext TestContext { get; }

    /// <summary>
    /// setup
    /// </summary>
    /// <param name="testContext"></param>
    public MailSendTest(TestContext testContext) =>
        TestContext = testContext;

    [Fact(DisplayName = "メール送信テスト")]
    public async Task SendMailTestAsync() {
        using var mailClient = await MailClient.CreateAsync(
            new(
                TestContext.GetConfiguration().GetAvailableValueByKey($"mailSettings:host"),
                int.Parse(TestContext.GetConfiguration().GetAvailableValueByKey($"mailSettings:port")),
                100000,
                null,
                null,
                "None"
            ));

        await mailClient.SendAsync(
            new(
                "テストタイトル",
                (isHtml: false, text: "テスト本文"),
                ("テスト差出人", "fromaddr@example.com"),
                new (string? name, string address)[] { (name: null, address: "toaddr@example.com") }.AsEnumerable())
        );
    }

    [Fact(DisplayName = "メール送信テスト")]
    public async Task SendMailTestAsync_CaseTextAndHtml() {
        using var mailClient = await MailClient.CreateAsync(
            new(
                TestContext.GetConfiguration().GetAvailableValueByKey($"mailSettings:host"),
                int.Parse(TestContext.GetConfiguration().GetAvailableValueByKey($"mailSettings:port")),
                100000,
                null,
                null,
                "None"
            ));

        await mailClient.SendAsync(
            MailMessage.CreateMultiPartMailMessage(
                "テストタイトルその２",
                (text: "テスト本文PlainText", html: "<p>テスト本文Html</p>"),
                ("テスト差出人", "fromaddr@example.com"),
                new (string? name, string address)[] { (name: null, address: "toaddr@example.com") }.AsEnumerable())
        );
    }

    /// <summary>
    /// Teardown
    /// </summary>
    public void Dispose() =>
        GC.SuppressFinalize(this);
}
