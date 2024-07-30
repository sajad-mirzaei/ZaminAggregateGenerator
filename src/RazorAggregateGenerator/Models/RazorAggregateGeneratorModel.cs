using System.ComponentModel.DataAnnotations;

namespace RazorAggregateGenerator.Models;

public class RazorAggregateGeneratorModel
{
    [StringLength(100, ErrorMessage = "فیلد ضروری است.")]
    public string AggregatePlural { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "فیلد ضروری است.")]
    public string AggregateName { get; set; } = string.Empty;

    public string? ProjectName { get; set; }

    [Required(ErrorMessage = "فیلد ضروری است.")]
    public string ProjectPath { get; set; } = string.Empty;

    public string AggregateClass { get; set; } = string.Empty;

    [Required(ErrorMessage = "فیلد ضروری است.")]
    public string ModuleName { get; set; }

    [Required(ErrorMessage = "فیلد ضروری است.")]
    public string UiFrameworkProjectName { get; set; } = String.Empty;
}