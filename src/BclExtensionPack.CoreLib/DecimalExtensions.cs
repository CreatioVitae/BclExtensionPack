namespace System;
public static class DecimalExtensions {
    public static int ToInt(this decimal target, RoundingType roundingType = RoundingType.EvenUp) =>
        roundingType switch {
            RoundingType.Up => Convert.ToInt32(Math.Ceiling(target)),
            RoundingType.Truncation => Convert.ToInt32(Math.Floor(target)),
            RoundingType.EvenUp => Convert.ToInt32(target),
            _ => throw new NotSupportedException()
        };
}

public enum RoundingType {
    Up,
    Truncation,
    EvenUp
}
