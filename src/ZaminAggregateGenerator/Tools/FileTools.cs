namespace ZaminAggregateGenerator.Tools;

internal static class FileTools
{
    public static string[] FilesList(string projectPath, string format, bool withSubFolders, List<string> checkContain, int indent = 0)
    {
        List<string> files = new();
        try
        {
            // List all directories and files in the current directory
            string[] dirs = Directory.GetDirectories(projectPath);
            var c = Directory.GetFiles(projectPath).ToList();
            if (c.Count > 0)
            {
                foreach (var item in c)
                {
                    if (item.EndsWith(format, StringComparison.OrdinalIgnoreCase))
                    {
                        if (checkContain.Count == 0 || checkContain.Count != 0 && CheckArrayItemsInStatement(checkContain, item))
                        {
                            files.Add(item);
                        }
                    }
                }

            }

            // Display the directories and files
            foreach (string dir in dirs)
            {
                Console.WriteLine(new string(' ', indent) + Path.GetFileName(dir));
                if (withSubFolders)
                    FilesList(dir, format, withSubFolders, checkContain, indent + 2); // Call the method recursively for each directory
            }

            foreach (string file in files)
            {
                Console.WriteLine(new string(' ', indent) + Path.GetFileName(file));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        var filesList = SortListForRunTimeError(files);
        return filesList;
    }

    public static string[] SortListForRunTimeError(List<string> collection)
    {
        if (collection.Count() > 0)
        {
            string[] sortedList = new string[6];
            foreach (var item in collection)
            {
                switch (item)
                {
                    case string s when s.Contains(".Domain"):
                        sortedList[0] = s;
                        break;
                    case string s when s.Contains(".Contracts"):
                        sortedList[1] = s;
                        break;
                    case string s when s.Contains(".ApplicationService"):
                        sortedList[2] = s;
                        break;
                    case string s when s.Contains("Sql.Commands"):
                        sortedList[3] = s;
                        break;
                    case string s when s.Contains("Sql.Queries"):
                        sortedList[4] = s;
                        break;
                    case string s when s.Contains("Endpoints"):
                        sortedList[5] = s;
                        break;
                    default:
                        break;
                }
            }
            return sortedList;
        }
        return null;
    }
    public static bool CheckArrayItemsInStatement(List<string> collection, string statement)
    {
        foreach (var item in collection)
        {
            if (statement.Contains(item))
                return true;
        }
        return false;
    }
}
