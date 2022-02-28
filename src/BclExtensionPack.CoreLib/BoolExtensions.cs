namespace System;

public static class BoolExtensions {
    public static int ToInt(this bool target) =>
        Convert.ToInt32(target);
}
