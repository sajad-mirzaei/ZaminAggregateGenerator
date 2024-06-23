using ZaminAggregateGenerator.Services;

internal class AggregateName__cshtml : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural";
    public string GetSourceCode() => @"@page
@using UiFramework.Resources
@using Zamin.Extensions.Translations.Abstractions
@model AggregateNameModel
@inject ITranslator Translator
@{
    ViewData[""Title""] = @Translator[ResourceKeys.ChangePasswordPeopleTitle];
}

<div class=""FormBox"">
    <div class=""HeaderBox"">
        AggregateName
    </div>
    <div class=""ContentBox"">
        <form method=""post"">
            <input type=""submit"" class=""btn btn-primary"" value=""Create"" />
        </form>
        <hr>
        <div id=""MyGridId""></div>
        @TempData[""SAPGridView""]
    </div>
</div>

@section Scripts {
    <script src=""AggregateName.cshtml.js""></script>
}
";
}