using RazorAggregateGenerator.Models;

namespace UI.Models;

public class RazorGeneratorViewModel
{
    public bool FormSubmit { get; set; } = false;
    public bool FormValidation { get; set; }
    public string? FormMessage { get; set; }
    public string AlertClass { get; set; } = "alert alert-danger";

    public RazorAggregateGeneratorModel RazorAggregateGeneratorModel { get; set; } = new();
}