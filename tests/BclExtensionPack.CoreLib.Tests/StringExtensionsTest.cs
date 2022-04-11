using System;
using Xunit;

namespace BclExtensionPack.CoreLib.Tests; 
public class StringExtensionsTest {
    [Fact]
    public void RemoveKeywordTest() =>
        Assert.Equal("Hoge", "HogeAsync".Remove("Async"));

    [Fact]
    public void RemoveKeywordTestNg() =>
    Assert.Equal("Hoge", "HogeAsyncAsync".Remove("Async"));
}
