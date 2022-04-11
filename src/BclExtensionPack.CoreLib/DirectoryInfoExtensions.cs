namespace System.IO;

public static class DirectoryInfoExtensions {
    public static Task DeleteAsync(this DirectoryInfo directoryInfo, bool? recursive = null) =>
        Task.Factory.StartNew(() => {
            if (!directoryInfo.Exists) { return; }

            if (recursive is null) {
                directoryInfo.Delete();
                return;
            }

            directoryInfo.Delete(recursive.Value);
        });
}
