using System.ComponentModel.DataAnnotations;

namespace ZaminAggregateGenerator.Models;

public class AggregateGeneratorModel
{
    [StringLength(100, ErrorMessage = "فیلد ضروری است.")]
    public string AggregatePlural { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "فیلد ضروری است.")]
    public string AggregateName { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "فیلد ضروری است.")]
    public string ProjectName { get; set; } = string.Empty;

    [Required(ErrorMessage = "فیلد ضروری است.")]
    public string ProjectPath { get; set; } = string.Empty;

    public string AggregateClass { get; set; } = string.Empty;
    public string? CommandDbContextPath { get; set; }
    public string? QueryDbContextPath { get; set; }
}