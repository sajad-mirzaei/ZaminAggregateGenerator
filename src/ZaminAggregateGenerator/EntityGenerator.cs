using System.Text;
using ZaminAggregateGenerator.Models;
using ZaminAggregateGenerator.Services;

namespace ZaminAggregateGenerator;

public class EntityGenerator
{
    internal EntityGeneratorModel GenModel { get; set; }
    internal List<PropertyModel> PropertyArray { get; set; }
    internal List<string> CsprojFilesList { get; set; }
    internal string DbContextPrefix { get; set; }
    internal string CommandDbContextPath { get; set; }
    internal string QueryDbContextPath { get; set; }
    internal Dictionary<string, string> AggFiles = new()
    {
        { "IAggregateNameQueryRepository", "AggregatePlural/Queries/IAggregateNameQueryRepository.cs" },
        { "AggregateName", "AggregatePlural/Entities/AggregateName.cs" },
        { "AggregateNameConfig", "AggregatePlural/Configs/AggregateNameConfig.cs" },
        { "AggregateNameQueryRepository", "AggregatePlural/AggregateNameQueryRepository.cs" },
        { "AggregateNameController", "AggregatePlural/AggregateNameController.cs" }
    };

    public EntityGenerator(EntityGeneratorModel genModel)
    {
        GenModel = genModel;
        PropertyArray = Extensions.ClassParse(GenModel.EntityClass);
        CsprojFilesList = FileTools.CsprojFilesList(GenModel.ProjectPath);
        AggFilesSetRealNames();
    }

    public string Generate()
    {
        ResultModel resultModel = this.EntityGeneratorValidation();
        if (resultModel.Result == false)
            return resultModel.GetString();
        SetDbContexts();
        SetDbContextPrefix();
        foreach (string csprojFilePath in CsprojFilesList)
        {
            var csprojFileDirectoryPath = Path.GetDirectoryName(csprojFilePath) ?? "";
            string csprojFileName = Path.GetFileName(csprojFilePath);
            List<ISourceCode> templateLayerFiles = GetTemplateLayerFiles(csprojFileName, csprojFileDirectoryPath);
            if (templateLayerFiles == null) continue;
            foreach (ISourceCode templateLayerFile in templateLayerFiles)
            {
                var classPath = templateLayerFile.GetClassPath();
                var sourceCode = templateLayerFile.GetSourceCode();

                sourceCode = ReplaceDbContextClassNames(sourceCode);
                classPath = ReplaceAggregateName(classPath);
                sourceCode = ReplaceAggregateName(sourceCode);
                sourceCode = TemplateContentChange(sourceCode);
                var targetFileName = GetCorrectClassName(templateLayerFile.GetType().Name);

                DirectoryTools.CreateNestedDirectories(classPath, csprojFileDirectoryPath);

                var targetDirectoryPath = Path.Combine(csprojFileDirectoryPath, classPath);
                var targetFilePath = Path.Combine(targetDirectoryPath, targetFileName);

                File.WriteAllText(targetFilePath, sourceCode, Encoding.Default);
            }
        }
        AddDbSetToDbContexts();
        AddEntityChangesToAggregate();
        return resultModel.GetString();
    }

    private List<ISourceCode>? GetTemplateLayerFiles(string fileName, string csprojFileDirectoryPath)
    {
        var layerName = fileName switch
        {
            string s when s.Contains("Core.ApplicationService") => "Core.ApplicationServices",
            string s when s.Contains("Core.Contracts") => AddAggFilesProjectRoot("Core.Contracts", csprojFileDirectoryPath, "IAggregateNameQueryRepository"),
            string s when s.Contains("Core.Domain") && !s.ToLower().Contains("domainservice") => AddAggFilesProjectRoot("Core.Domain", csprojFileDirectoryPath, "AggregateName"),
            string s when s.Contains("Sql.Commands") => AddAggFilesProjectRoot("Sql.Commands", csprojFileDirectoryPath, "AggregateNameConfig"),
            string s when s.Contains("Sql.Queries") => AddAggFilesProjectRoot("Sql.Queries", csprojFileDirectoryPath, "AggregateNameQueryRepository"),
            string s when s.Contains("Endpoints") => AddAggFilesProjectRoot("Endpoints", csprojFileDirectoryPath, "AggregateNameController"),

            //Consider misspellings
            string s when s.Contains("Core.ApplicatoinService") => "Core.ApplicationServices",
            string s when s.Contains("Core.ApplicationService") => "Core.ApplicationServices",
            string s when s.Contains("Core.Contract") => AddAggFilesProjectRoot("Core.Contracts", csprojFileDirectoryPath, "IAggregateNameQueryRepository"),
            string s when s.Contains("Core.Domains") && !s.ToLower().Contains("domainservice") => AddAggFilesProjectRoot("Core.Domain", s, "AggregateName"),
            string s when s.Contains("Sql.Command") => AddAggFilesProjectRoot("Sql.Commands", csprojFileDirectoryPath, "AggregateNameConfig"),
            string s when s.Contains("Sql.Querie") => AddAggFilesProjectRoot("Sql.Queries", csprojFileDirectoryPath, "AggregateNameQueryRepository"),
            string s when s.Contains("Sql.Query") => AddAggFilesProjectRoot("Sql.Queries", csprojFileDirectoryPath, "AggregateNameQueryRepository"),
            string s when s.Contains("EndPoints") => AddAggFilesProjectRoot("Endpoints", csprojFileDirectoryPath, "AggregateNameController"),
            string s when s.Contains("EndPoint") => AddAggFilesProjectRoot("Endpoints", csprojFileDirectoryPath, "AggregateNameController"),
            string s when s.Contains("Endpoint") => AddAggFilesProjectRoot("Endpoints", csprojFileDirectoryPath, "AggregateNameController"),

            //ToLower
            string s when s.Contains(("Core.ApplicationService").ToLower()) => "Core.ApplicationServices",
            string s when s.Contains(("Core.Contracts").ToLower()) => AddAggFilesProjectRoot("Core.Contracts", csprojFileDirectoryPath, "IAggregateNameQueryRepository"),
            string s when s.Contains(("Core.Domain").ToLower()) && !s.ToLower().Contains("domainservice") => AddAggFilesProjectRoot("Core.Domain", csprojFileDirectoryPath, "AggregateName"),
            string s when s.Contains(("Sql.Commands").ToLower()) => AddAggFilesProjectRoot("Sql.Commands", csprojFileDirectoryPath, "AggregateNameConfig"),
            string s when s.Contains(("Sql.Queries").ToLower()) => AddAggFilesProjectRoot("Sql.Queries", csprojFileDirectoryPath, "AggregateNameQueryRepository"),
            string s when s.Contains(("Endpoints").ToLower()) => AddAggFilesProjectRoot("Endpoints", csprojFileDirectoryPath, "AggregateNameController"),
            _ => null
        };
        return layerName == null ? null : EntityConfigs.LayerMappings[layerName];
    }
    void AggFilesSetRealNames()
    {
        foreach (var item in AggFiles)
        {
            AggFiles[item.Key] = ReplaceAggregateName(item.Value);
        }
    }
    string AddAggFilesProjectRoot(string layer, string projectRoot, string key)
    {
        if (AggFiles[key] != "")
            AggFiles[key] = projectRoot?.TrimEnd('/').TrimEnd('\\') + '\\' + AggFiles[key].Replace("/", "\\");
        return layer;
    }
    internal void AddEntityChangesToAggregate()
    {
        var o = new AddEntityTextsToAggregate();
        Dictionary<string, string> tmpTextsToReplaceInAggregate = new();
        foreach (var item in o.TextsToReplaceInAggregate)
        {
            var v = ReplaceAggregateName(item.Value);
            v = ReplaceAggregateName(v);
            EntityPropertyAdder replacementMethods = new(v, PropertyArray, GenModel);
            v = replacementMethods.AddProperties();
            tmpTextsToReplaceInAggregate[item.Key] = v + "\r\n//" + item.Key;
        }

        foreach (var pathItem in AggFiles)
        {
            string c = File.ReadAllText(pathItem.Value, Encoding.Default);
            foreach (var textItem in tmpTextsToReplaceInAggregate)
            {
                c = c.Replace("//" + textItem.Key, textItem.Value);
            }
            File.WriteAllText(pathItem.Value, c, Encoding.Default);
        }
    }
    void AddDbSetToDbContexts()
    {
        GenModel.CommandDbContextPath = CommandDbContextPath;
        GenModel.QueryDbContextPath = QueryDbContextPath;
        var projectName = GenModel.ProjectName != null ? GenModel.ProjectName + "." : "";

        string content1 = File.ReadAllText(GenModel.CommandDbContextPath, Encoding.Default);
        content1 = content1.Replace("//SqlCommandsCommandDbContextDbSet", "        public DbSet<" + GenModel.EntityName + "> " + GenModel.EntityPlural + " { get; set; }\r\n//SqlCommandsCommandDbContextDbSet");
        content1 = content1.Replace("//SqlCommandsCommandDbContextUsing", "using " + projectName + "Core.Domain." + GenModel.EntityPlural + ".Entities;\r\n//SqlCommandsCommandDbContextUsing");
        File.WriteAllText(GenModel.CommandDbContextPath, content1, Encoding.Default);

        string content2 = File.ReadAllText(GenModel.QueryDbContextPath, Encoding.Default);
        content2 = content2.Replace("//SqlQueriesQueryDbContextDbSet", "        public virtual DbSet<" + GenModel.EntityName + "> " + GenModel.EntityPlural + " { get; set; }\r\n//SqlQueriesQueryDbContextDbSet");
        File.WriteAllText(GenModel.QueryDbContextPath, content2, Encoding.Default);
    }
    internal string ReplaceAggregateName(string input)
    {
        var projectName = GenModel.ProjectName != null ? GenModel.ProjectName + "." : "";
        input = input.Replace("ProjectName.", projectName);
        var o = input
                    .Replace("IdTypeReplacement", GenModel.IdTypeReplacement.ToString().ToLower())
                    .Replace("AggregatePlural", GenModel.AggregatePlural)
                    .Replace("aggregatePlural", GenModel.AggregatePlural.ToLowerFirstChar())
                    .Replace("AggregateName", GenModel.AggregateName)
                    .Replace("aggregateName", GenModel.AggregateName.ToLowerFirstChar())
                    .Replace("EntityPlural", GenModel.EntityPlural)
                    .Replace("entityPlural", GenModel.EntityPlural.ToLowerFirstChar())
                    .Replace("EntityName", GenModel.EntityName)
                    .Replace("entityName", GenModel.EntityName.ToLowerFirstChar());
        return o;
    }
    internal string TemplateContentChange(string c)
    {
        EntityPropertyAdder replacementMethods = new(c, PropertyArray, GenModel);
        return replacementMethods.AddProperties();
    }
    public string GetCorrectClassName(string className)
    {
        var name = className.Contains('_') ? className.Split('_')[0] + ".cs" : className + ".cs";
        return ReplaceAggregateName(name);
    }
    public void SetDbContexts()
    {
        var dbContextFilesList = FileTools.DbContextFilesList(GenModel.ProjectPath);
        CommandDbContextPath ??= dbContextFilesList["CommandDbContext"];
        QueryDbContextPath ??= dbContextFilesList["QueryDbContext"];
    }
    public void SetDbContextPrefix()
    {
        var fileName = Path.GetFileNameWithoutExtension(CommandDbContextPath);
        var p = fileName.Replace("CommandDbContext", "");
        DbContextPrefix = p.Replace("QueryDbContext", "");
    }
    public string ReplaceDbContextClassNames(string input)
    {
        input = input.Replace("ProjectNameCommandDbContext", DbContextPrefix + "CommandDbContext");
        input = input.Replace("ProjectNameQueryDbContext", DbContextPrefix + "QueryDbContext");
        return input;
    }
}

public class AddEntityTextsToAggregate
{
    public Dictionary<string, string> TextsToReplaceInAggregate = new()
    {
        {
            "EntityPropertiesReplacementText", @"
private List<EntityName> _entityPlural = new List<EntityName>();
public IReadOnlyCollection<EntityName> EntityPlural => _entityPlural.ToList();"
        },
        {
            "EntityMethodsReplacementText", @"
#region EntityPlural
public EntityName AddEntityName(
EntityDomainReplacementText2
)
{
    var entity = EntityName.Create(
EntityDomainReplacementText4
    );
    _entityPlural.Add(entity);
    AddEvent(new EntityNameAdded(
        Id,
EntityDomainReplacementText6
    ));

    return entity;
}
#endregion"
        },
        {
            "EntityIQueryRepositoryReplacementText", @"
Task<EntityNameByIdDto> SelectAsync(GetEntityNameByIdQuery request);
Task<PagedData<EntityNameDto>> SelectAsync(GetEntityNameQuery request);"
        },
        {
            "EntityIQueryRepositoryUsingReplacementText", @"
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityNameById;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityPlural;"
        },
        {
            "EntityQueryRepositoryUsingReplacementText", @"
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityNameById;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityPlural;"
        },
        {
            "EntityControllerUsingReplacementText", @"
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityNameById;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityPlural;
using ProjectName.Core.Contracts.AggregatePlural.Commands.AddEntityName;"
        },
        {
            "EntityCommandConfigReplacementText",
            @"builder.HasMany<EntityName>().WithOne().HasPrincipalKey(c => c.Id).HasForeignKey(""AggregateNameId"");"
        },
        {
            "EntityQueryRepositoryReplacementText",
            @"
public async Task<EntityNameByIdDto> SelectAsync(GetEntityNameByIdQuery request)
{
    return await _dbContext.EntityPlural.Select(c => new EntityNameByIdDto()
    {
        Id = c.Id,
EntitySqlQueriesReplacementText1
    }).SingleOrDefaultAsync(c => c.Id.Equals(request.Id));
}
public async Task<PagedData<EntityNameDto>> SelectAsync(GetEntityNameQuery request)
{
    #region Query
    var query = _dbContext.EntityPlural.AsQueryable();
    #endregion

    #region Filters
EntitySqlQueriesReplacementText2
    #endregion

    #region Result
    PagedData<EntityNameDto> result = await query.ToPagedData(request, c => new EntityNameDto
    {
        Id = c.Id,
EntitySqlQueriesReplacementText1
    });

    return result;
    #endregion
}"
        },
        {
            "EntityControllerMethodsReplacementText",
            @"
#region EntityPlural
[HttpPost(""createEntityName"")]
public async Task<IActionResult> CreateEntityName([FromBody] AddEntityNameCommand createEntityName)
{
    return await Create<AddEntityNameCommand, IdTypeReplacement>(createEntityName);
}

[HttpGet(""getEntityPlural"")]
public async Task<IActionResult> GetEntityName([FromQuery] GetEntityNameQuery query)
{
    return await Query<GetEntityNameQuery, PagedData<EntityNameDto>>(query);
}

[HttpGet(""getEntityNameById"")]
public async Task<IActionResult> GetEntityNameById([FromQuery] GetEntityNameByIdQuery query)
{
    return await Query<GetEntityNameByIdQuery, EntityNameByIdDto>(query);
}
#endregion
"
        }
    };
}