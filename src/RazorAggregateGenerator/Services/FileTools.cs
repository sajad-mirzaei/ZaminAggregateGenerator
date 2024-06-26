﻿namespace RazorAggregateGenerator.Services;

internal static class FileTools
{
    public static List<string> RootFoldersList(string projectPath)
    {
        var foldersList = new List<string>();
        if (Directory.Exists(projectPath))
        {
            var directories = Directory.GetDirectories(projectPath, "*", SearchOption.TopDirectoryOnly);
            foldersList.AddRange(directories);
        }
        else
        {
            Console.WriteLine($"The directory '{projectPath}' does not exist.");
        }

        return foldersList;
    }
    public static Dictionary<string, string> DbContextFilesList(string projectPath)
    {
        Dictionary<string, string> dbContextFiles = new();
        try
        {
            var commandDbContext = Directory.EnumerateFiles(projectPath, "*CommandDbContext.cs", SearchOption.AllDirectories).ToList();
            dbContextFiles.Add("CommandDbContext", commandDbContext.Count() > 0 ? commandDbContext[0] : "");
            var queryDbContext = Directory.EnumerateFiles(projectPath, "*QueryDbContext.cs", SearchOption.AllDirectories).ToList();
            dbContextFiles.Add("QueryDbContext", queryDbContext.Count() > 0 ? queryDbContext[0] : "");
        }
        catch (Exception ex)
        {
            throw new Exception("Error: " + ex.Message);
        }

        return dbContextFiles;
    }
    /*private static List<string> GenerateFilesInSafeOrder(List<string> collection)
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
    }*/
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
