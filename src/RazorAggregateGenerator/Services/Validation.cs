namespace RazorAggregateGenerator.Services;
internal static class Validation
{
    public static ResultModel Result { get; set; } = new ResultModel();
    internal static ResultModel AggregateGeneratorValidation(this RazorAggregateGenerator aggregateGenerator)
    {
        /*if (aggregateGenerator.CsprojFilesList.Count == 0)
        {
            Result.Message = "Project path is empty";
            Result.Result = false;
        }
        return Result;*/
        return new ResultModel() { Result = true };
    }
}