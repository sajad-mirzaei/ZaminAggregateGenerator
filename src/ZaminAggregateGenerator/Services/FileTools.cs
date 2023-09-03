namespace ZaminAggregateGenerator.Services;

internal static class FileTools
{
    public static List<string> CsprojFilesList(string projectPath)
    {
        var resultFiles = new List<string>();
        try
        {
            foreach (var filePath in Directory.EnumerateFiles(projectPath, "*.csproj", SearchOption.AllDirectories))
            {
                resultFiles.Add(filePath);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error: " + ex.Message);
        }

        var filesList = GenerateFilesInSafeOrder(resultFiles);
        return filesList;
    }
    private static List<string> GenerateFilesInSafeOrder(List<string> collection)
    {
        if (collection.Count == 0)
        {
            return new List<string>();
        }

        var orderedList = collection.OrderBy(item =>
        {
            if (item.Contains("Core.Domain"))
                return 0;
            if (item.Contains("Core.Contracts"))
                return 1;
            if (item.Contains("Core.ApplicationService"))
                return 2;
            if (item.Contains("Sql.Commands"))
                return 3;
            if (item.Contains("Sql.Queries"))
                return 4;
            if (item.Contains("Endpoints"))
                return 5;
            return 6;
        }).ToList();

        return orderedList;
    }
    private static bool CheckArrayItemsInStatement(List<string> collection, string statement)
    {
        foreach (var item in collection)
        {
            if (statement.Contains(item))
                return true;
        }
        return false;
    }
}
