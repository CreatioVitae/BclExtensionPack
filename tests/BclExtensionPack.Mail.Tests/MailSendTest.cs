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

    [Fact(DisplayName = "���[�����M�e�X�g")]
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
                "�e�X�g�^�C�g��",
                (isHtml: false, text: "�e�X�g�{��"),
                ("�e�X�g���o�l", "fromaddr@example.com"),
                new (string? name, string address)[] { (name: null, address: "toaddr@example.com") }.AsEnumerable())
        );
    }

    [Fact(DisplayName = "���[�����M�e�X�g")]
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
                "�e�X�g�^�C�g�����̂Q",
                (text: "�e�X�g�{��PlainText", html: "<p>�e�X�g�{��Html</p>"),
                ("�e�X�g���o�l", "fromaddr@example.com"),
                new (string? name, string address)[] { (name: null, address: "toaddr@example.com") }.AsEnumerable())
        );
    }

    /// <summary>
    /// Teardown
    /// </summary>
    public void Dispose() =>
        GC.SuppressFinalize(this);
}
