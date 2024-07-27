using System.ComponentModel.DataAnnotations;

namespace ZaminAggregateGenerator.Models;

public class ScaffoldServiceModel
{
    [StringLength(400, ErrorMessage = "فیلد ضروری است.")]
    public string ConnectionString { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "فیلد ضروری است.")]
    public string SchemaName { get; set; } = string.Empty;

    [Required(ErrorMessage = "فیلد ضروری است.")]
    public string TableName { get; set; } = string.Empty;
}