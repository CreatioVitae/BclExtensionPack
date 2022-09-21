using System;
using Xunit;

namespace BclExtensionPack.CoreLib.Tests; 
public class DecimalExtensionsTest {
    [Fact]
    public void ToIntCaseEvenUp() {
        //RoundingType.EvenUp If Default Parameter...
        Assert.Equal(2, 2.5m.ToInt());
        Assert.Equal(2, 2.5m.ToInt(RoundingType.EvenUp));
    }

    [Fact]
    public void ToIntCaseUp() {
        Assert.Equal(2, 2.0m.ToInt(RoundingType.Up));
        Assert.Equal(3, 2.1m.ToInt(RoundingType.Up));
        Assert.Equal(3, 2.2m.ToInt(RoundingType.Up));
        Assert.Equal(3, 2.3m.ToInt(RoundingType.Up));
        Assert.Equal(3, 2.4m.ToInt(RoundingType.Up));
        Assert.Equal(3, 2.5m.ToInt(RoundingType.Up));
        Assert.Equal(3, 2.6m.ToInt(RoundingType.Up));
        Assert.Equal(3, 2.7m.ToInt(RoundingType.Up));
        Assert.Equal(3, 2.8m.ToInt(RoundingType.Up));
        Assert.Equal(3, 2.9m.ToInt(RoundingType.Up));
    }

    [Fact]
    public void ToIntCaseTruncation() {
        Assert.Equal(2, 2.0m.ToInt(RoundingType.Truncation));
        Assert.Equal(2, 2.1m.ToInt(RoundingType.Truncation));
        Assert.Equal(2, 2.2m.ToInt(RoundingType.Truncation));
        Assert.Equal(2, 2.3m.ToInt(RoundingType.Truncation));
        Assert.Equal(2, 2.4m.ToInt(RoundingType.Truncation));
        Assert.Equal(2, 2.5m.ToInt(RoundingType.Truncation));
        Assert.Equal(2, 2.6m.ToInt(RoundingType.Truncation));
        Assert.Equal(2, 2.7m.ToInt(RoundingType.Truncation));
        Assert.Equal(2, 2.8m.ToInt(RoundingType.Truncation));
        Assert.Equal(2, 2.9m.ToInt(RoundingType.Truncation));
    }
}
