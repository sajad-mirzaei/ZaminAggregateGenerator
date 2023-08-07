using System.Reflection;
using System.Text;

namespace ZaminAggregateGenerator;

internal class MoveTemplate
{
    public string ProjectName { get; set; } //= "Voc";
    public string AggregatePlural { get; set; } //= "OldTable2s";
    public string AggregateName { get; set; } //= "OldTable2";
    public string TargetPath { get; set; } //= 
    public string TemplateFolder { get; set; } //= "Endpoints.API";
    private string TemplatePath { get; set; }

    public void Exex()
    {
        //var thisProjectPath = GetProjectDirectoryPath();
        var thisProjectPath = "D:\\.NET\\Github\\MyGithub\\voc\\ZaminAggregateGenerator";
        TemplatePath = thisProjectPath + "\\Template\\" + TemplateFolder;
        //TemplatePath = "D:\\.NET\\Github\\MyGithub\\voc\\ZaminAggregateGenerator\\Template";
        try
        {
            CopyDirectory();
            ReplaceTextInDirectory();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    void CopyDirectory()
    {
        foreach (string dirPath in Directory.GetDirectories(TemplatePath, "*", SearchOption.AllDirectories))
        {
            var newDirPath = ReplaceAggregateName(dirPath);
            Directory.CreateDirectory(newDirPath.Replace(TemplatePath, TargetPath));
        }

        foreach (string sourceFilePath in Directory.GetFiles(TemplatePath, "*.*", SearchOption.AllDirectories))
        {
            var sourceFilePathReplaced = ReplaceAggregateName(sourceFilePath);
            var destFileName = sourceFilePathReplaced.Replace(TemplatePath, TargetPath);
            destFileName = destFileName.Replace(".csharp", ".cs");
            File.Copy(sourceFilePath, destFileName, true);
        }
    }
    void ReplaceTextInDirectory()
    {
        var newTargetPath = TargetPath + "\\" + AggregatePlural;
        foreach (string file in Directory.GetFiles(newTargetPath, "*.*", SearchOption.AllDirectories))
        {
            string content = File.ReadAllText(file, Encoding.UTF8);

            content = ReplaceAggregateName(content);
            string newFile = ReplaceAggregateName(file);

            File.WriteAllText(newFile, content, Encoding.UTF8);
        }
    }
    public string ReplaceAggregateName(string input)
    {
        return input
                    .Replace("AggregatePlural", AggregatePlural)
                    .Replace("aggregatePlural", ToLowerFirstChar(AggregatePlural))
                    .Replace("AggregateName", AggregateName)
                    .Replace("aggregateName", ToLowerFirstChar(AggregateName))
                    .Replace("ProjectName", ProjectName)
                    .Replace("projectName", ToLowerFirstChar(ProjectName));
    }
    public string ToLowerFirstChar(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return char.ToLower(input[0]) + input.Substring(1);
    }
    public string GetTemplateFilePath()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string resourceName = "Template";

        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null)
            {
                throw new ArgumentException("Template file not found: ");
            }

            string tempPath = Path.Combine(Path.GetTempPath(), "");
            using (FileStream fileStream = File.Create(tempPath))
            {
                stream.CopyTo(fileStream);
            }

            return tempPath;
        }
    }
    public string GetProjectDirectoryPath()
    {
        string codeBase = Assembly.GetExecutingAssembly().CodeBase;
        UriBuilder uri = new UriBuilder(codeBase);
        string path = Uri.UnescapeDataString(uri.Path);
        return Path.GetDirectoryName(path);
    }
}
