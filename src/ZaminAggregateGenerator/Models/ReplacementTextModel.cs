namespace ZaminAggregateGenerator.Models;

internal class ReplacementTextModel
{
    //public string OldValue { get; private set; }
    public string LeftPadding { get; private set; }
    public string LineBreak { get; private set; }

    public ReplacementTextModel(/*string oldValue, */int leftPadding = 4, string lineBreak = "\n")
    {
        //OldValue = oldValue;
        LeftPadding = GetSpaces(leftPadding);
        LineBreak = lineBreak;
    }
    private static string GetSpaces(int leftPadding)
    {
        return new string(' ', leftPadding);
    }
}
