using Xunit.Abstractions;
using Xunit.Sdk;
using Microsoft.Extensions.Configuration;

[assembly: TestFramework("BclExtensionPack.Mail.Tests.AssemblyInitializer", "BclExtensionPack.Mail.Tests")]
namespace BclExtensionPack.Mail.Tests;

public class AssemblyInitializer : XunitTestFramework, IDisposable {
    public static IConfiguration Configuration { get; set; } = null!;

    public AssemblyInitializer(IMessageSink messageSink) : base(messageSink) {
        var testRootPath = new FileInfo(typeof(AssemblyInitializer).Assembly.Location).Directory?.FullName ?? throw new InvalidProgramException();
        Configuration = new ConfigurationBuilder()
            .SetBasePath(testRootPath).AddJsonFile("appsettings.Test.json", optional: false).AddEnvironmentVariables().Build();
    }

    public new void Dispose() {
        base.Dispose();
        GC.SuppressFinalize(this);
    }
}
