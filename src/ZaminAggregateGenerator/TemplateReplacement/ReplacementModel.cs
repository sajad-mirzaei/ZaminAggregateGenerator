namespace ZaminAggregateGenerator.TemplateReplacement;


internal class TextReplacementDomain
{
    private readonly List<ICommand> _commands = new List<ICommand>();
    private string _content;

    public TextReplacementDomain(string filePath, List<ReplacementModel> replacements)
    {
        _content = ReadFileContent(filePath);

        InitializeCommands(replacements);
    }

    private void InitializeCommands(List<ReplacementModel> replacements)
    {
        foreach (var replacement in replacements)
        {
            _commands.Add(new TextReplaceCommand(replacement.SearchText, replacement.ReplaceText));
        }
    }

    public string ExecuteReplacements()
    {
        foreach (var command in _commands)
        {
            _content = command.Execute(_content);
        }
        return _content;
    }

    private string ReadFileContent(string filePath)
    {
        try
        {
            return File.ReadAllText(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading file: " + ex.Message);
            return string.Empty;
        }
    }
}

internal interface ICommand
{
    string Execute(string content);
}

internal class TextReplaceCommand : ICommand
{
    private readonly string _searchText;
    private readonly string _replaceText;

    public TextReplaceCommand(string searchText, string replaceText)
    {
        _searchText = searchText;
        _replaceText = replaceText;
    }

    public string Execute(string content)
    {
        return content.Replace(_searchText, _replaceText);
    }
}

internal class ReplacementModel
{
    public string SearchText { get; set; }
    public string ReplaceText { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        string filePath = "path_to_your_file.txt"; // مسیر فایل
        var replacements = new List<ReplacementModel>
        {
            new ReplacementModel { SearchText = "old_text1", ReplaceText = "new_text1" },
            new ReplacementModel { SearchText = "old_text2", ReplaceText = "new_text2" },
            // اضافه کردن عبارات و متن‌های جایگزین دیگر
        };

        var domain = new TextReplacementDomain(filePath, replacements);
        string updatedContent = domain.ExecuteReplacements();

        Console.WriteLine(updatedContent);
    }
}
