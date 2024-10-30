using ZaminAggregateGenerator.Models;

namespace UI.Models;

public class AddEntityViewModel
{
    public bool FormSubmit { get; set; } = false;
    public bool FormValidation { get; set; }
    public string? FormMessage { get; set; }
    public string AlertClass { get; set; } = "alert alert-danger";

    public EntityGeneratorModel EntityGeneratorModel { get; set; } = new EntityGeneratorModel();
}