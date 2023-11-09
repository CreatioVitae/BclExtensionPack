namespace BclExtensionPack.Mail.Tests.TestContexts;

public class TestContext : IDisposable {
    public IConfiguration GetConfiguration() =>
        AssemblyInitializer.Configuration;

    public void Dispose() =>
        GC.SuppressFinalize(this);
}
