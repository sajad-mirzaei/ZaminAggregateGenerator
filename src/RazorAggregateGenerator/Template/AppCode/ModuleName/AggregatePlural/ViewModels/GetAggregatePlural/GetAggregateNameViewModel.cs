﻿using ZaminAggregateGenerator.Services;

internal class GetAggregateNameViewModel : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural\ViewModels\GetAggregatePlural";
    public string GetSourceCode() => @"namespace ProjectName.AppCode.ModuleName.AggregatePlural.ViewModels.GetAggregatePlural;

public class GetAggregateNameViewModel : BaseViewModel
{
AppCodeReplacementText1
}
";
}