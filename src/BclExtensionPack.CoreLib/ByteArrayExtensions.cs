using System.IO;

namespace BclExtensionPack.CoreLib;

public static class ByteArrayExtensions {
    public static Task ForceWriteAllBytesAsync(this byte[] binary, string writeToPath) {
        var writeToDirectory = Path.GetDirectoryName(writeToPath);

        ArgumentNullException.ThrowIfNull(writeToDirectory);

        if (!Directory.Exists(writeToDirectory)) {
            Directory.CreateDirectory(writeToDirectory);
        }

        return File.WriteAllBytesAsync(writeToPath, binary);
    }
}
