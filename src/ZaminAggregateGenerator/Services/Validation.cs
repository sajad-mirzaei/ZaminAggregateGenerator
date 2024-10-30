namespace ZaminAggregateGenerator.Services;
internal static class Validation
{
    public static ResultModel Result { get; set; } = new ResultModel();
    internal static ResultModel AggregateGeneratorValidation(this AggregateGenerator aggregateGenerator)
    {
        if (aggregateGenerator.CsprojFilesList.Count == 0)
        {
            Result.Message = "Project path is empty";
            Result.Result = false;
        }
        return Result;
    }

    internal static ResultModel EntityGeneratorValidation(this EntityGenerator entityGenerator)
    {
        if (entityGenerator.CsprojFilesList.Count == 0)
        {
            Result.Message = "Project path is empty";
            Result.Result = false;
        }
        return Result;
    }
}