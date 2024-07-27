using Microsoft.Data.SqlClient;
using System.Text;
using ZaminAggregateGenerator.Models;

namespace ZaminAggregateGenerator.Services;

public static class ScaffoldServices
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fetchEntityFromSqlModel"></param>
    /// <returns></returns>
    public static string GetFromSql(ScaffoldServiceModel fetchEntityFromSqlModel)
    {
        string connectionString = fetchEntityFromSqlModel.ConnectionString;
        string schemaName = fetchEntityFromSqlModel.SchemaName;
        string className = fetchEntityFromSqlModel.TableName;
        string tableName = fetchEntityFromSqlModel.TableName;
        var sb = new StringBuilder();

        sb.AppendLine($"public class {className}");
        sb.AppendLine("{");

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var query = @"SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE 
                          FROM INFORMATION_SCHEMA.COLUMNS 
                          WHERE TABLE_SCHEMA = @SchemaName AND TABLE_NAME = @TableName";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@SchemaName", schemaName);
                command.Parameters.AddWithValue("@TableName", tableName);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var columnName = reader["COLUMN_NAME"].ToString();
                        var dataType = GetCSharpDataType(reader["DATA_TYPE"].ToString(), reader["IS_NULLABLE"].ToString() == "YES");
                        if (columnName?.ToLower() != "id")
                            sb.AppendLine($"    public {dataType} {columnName} {{ get; set; }}");
                    }
                }
            }
        }

        sb.AppendLine("}");
        return sb.ToString();
    }

    private static string GetCSharpDataType(string sqlDataType, bool isNullable)
    {
        string cSharpType;

        switch (sqlDataType)
        {
            case "int":
                cSharpType = "int";
                break;
            case "bigint":
                cSharpType = "long";
                break;
            case "smallint":
                cSharpType = "short";
                break;
            case "tinyint":
                cSharpType = "byte";
                break;
            case "bit":
                cSharpType = "bool";
                break;
            case "decimal":
            case "numeric":
            case "money":
            case "smallmoney":
                cSharpType = "decimal";
                break;
            case "float":
                cSharpType = "double";
                break;
            case "real":
                cSharpType = "float";
                break;
            case "date":
            case "datetime":
            case "datetime2":
            case "smalldatetime":
            case "datetimeoffset":
                cSharpType = "DateTime";
                break;
            case "time":
                cSharpType = "TimeSpan";
                break;
            case "char":
            case "varchar":
            case "text":
            case "nchar":
            case "nvarchar":
            case "ntext":
                cSharpType = "string";
                break;
            case "binary":
            case "varbinary":
            case "image":
                cSharpType = "byte[]";
                break;
            default:
                cSharpType = "string";
                break;
        }

        if (isNullable /* && cSharpType != "string" && cSharpType != "byte[]"*/)
        {
            cSharpType += "?";
        }

        return cSharpType;
    }

    private static void SaveClassToFile(string classContent, string filePath)
    {
        File.WriteAllText(filePath, classContent);
    }
}