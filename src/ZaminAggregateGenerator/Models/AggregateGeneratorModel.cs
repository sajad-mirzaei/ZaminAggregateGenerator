using System.ComponentModel.DataAnnotations;

namespace ZaminAggregateGenerator.Models;

public class AggregateGeneratorModel
{
    [StringLength(100, ErrorMessage = "فیلد ضروری است.")]
    public string AggregatePlural { get; set; }

    [StringLength(100, ErrorMessage = "فیلد ضروری است.")]
    public string AggregateName { get; set; }

    [StringLength(100, ErrorMessage = "فیلد ضروری است.")]
    public string ProjectName { get; set; }

    [Required(ErrorMessage = "فیلد ضروری است.")]
    public string ProjectPath { get; set; }

    public string AggregateClass { get; set; }
}
