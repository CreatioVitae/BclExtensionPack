using System;
using Xunit;

namespace BclExtensionPack.CoreLib.Tests; 
public class StringExtensionsTest {
    [Fact]
    public void RemoveKeywordTest() =>
        Assert.Equal("Hoge", "HogeAsync".Remove("Async"));

    [Fact]
    public void RemoveAllKeywordTest() =>
    Assert.Equal("Hoge", "HogeAsyncAsync".Remove("Async"));

    [Fact]
    public void RemoveFirstKeywordTest() =>
    Assert.Equal("HogeAsync", "AsyncHogeAsync".RemoveFirst("Async"));
}
