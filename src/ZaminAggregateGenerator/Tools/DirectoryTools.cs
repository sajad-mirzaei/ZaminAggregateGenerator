namespace ZaminAggregateGenerator.Tools;

internal static class DirectoryTools
{
    public static void CreatePathDirectories(string path, string csprojFileDirectoryPath)
    {
        var directoryList = path.Split('\\');
        CreateSubdirectories(csprojFileDirectoryPath, directoryList, 0);
    }
    private static void CreateSubdirectories(string parentPath, string[] directories, int index)
    {
        if (index >= directories.Length || string.IsNullOrWhiteSpace(directories[index]))
            return;

        var targetDirectoryName = RemoveInvalidPathChars(directories[index]);
        var targetDirectoryPath = Path.Combine(parentPath, targetDirectoryName);

        if (!Directory.Exists(targetDirectoryPath))
        {
            Directory.CreateDirectory(targetDirectoryPath);
        }

        CreateSubdirectories(targetDirectoryPath, directories, index + 1);
    }
    private static string RemoveInvalidPathChars(string input)
    {
        var invalidChars = Path.GetInvalidPathChars();
        return new string(input.Where(c => !invalidChars.Contains(c)).ToArray());
    }
}
