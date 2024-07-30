namespace ZaminAggregateGenerator.Models;

internal class TextReplacementModel
{
    public string OldValue { get; set; } = string.Empty;
    public string LeftPadding { get; set; }
    public string LineBreak { get; set; }
    public char[]? TrimEnd { get; set; }
    public PropertyModel PropertyModel { get; set; } = new();

    public TextReplacementModel(
        int leftPadding = 4,
        char[]? trimEnd = null,
        string lineBreak = "\r\n"
        )
    {
        LeftPadding = GetSpaces(leftPadding);
        LineBreak = lineBreak;
        TrimEnd = trimEnd;
    }
    private static string GetSpaces(int leftPadding) => new string(' ', leftPadding);
}
