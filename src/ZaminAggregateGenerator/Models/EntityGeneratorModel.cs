using System.ComponentModel.DataAnnotations;

namespace ZaminAggregateGenerator.Models;

public class EntityGeneratorModel
{
    [StringLength(100, ErrorMessage = "فیلد ضروری است.")]
    public string AggregatePlural { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "فیلد ضروری است.")]
    public string AggregateName { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "فیلد ضروری است.")]
    public string EntityPlural { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "فیلد ضروری است.")]
    public string EntityName { get; set; } = string.Empty;

    public string? ProjectName { get; set; }

    [Required(ErrorMessage = "فیلد ضروری است.")]
    public string ProjectPath { get; set; } = string.Empty;

    public string EntityClass { get; set; } = string.Empty;
    public string? CommandDbContextPath { get; set; }
    public string? QueryDbContextPath { get; set; }
    public DisableShadowPropertyEnum DisableShadowProperty { get; set; } = DisableShadowPropertyEnum.True;

    /// <summary> int or long</summary>
    public IdTypeReplacementEnum IdTypeReplacement { get; set; } = IdTypeReplacementEnum.Int;
}