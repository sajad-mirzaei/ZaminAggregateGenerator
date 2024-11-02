# A Package to quickly create the files of an aggregate in the [Zamin project](https://github.com/oroumand/Zamin)


[Zamin](https://github.com/oroumand/Zamin)AggregationGenerator is a NuGet package designed to streamline and accelerate the process of creating Aggregation files across various layers of your [Api project](https://github.com/sajad-mirzaei/ZaminSample1). Aggregation files play a crucial role in combining related data and facilitating efficient data retrieval in modern software systems. However, generating these files manually can be time-consuming and error-prone. AggregationGenerator aims to provide a seamless solution to this challenge by automating.

## Getting Started:
Getting started with AggregationGenerator is straightforward. Simply install the NuGet package into your [Api project](https://github.com/sajad-mirzaei/ZaminSample1) and follow the provided documentation to configure and utilize the package effectively. With AggregationGenerator, you'll experience improved code organization, enhanced data retrieval capabilities, and an optimized development workflow.
Getting started with Aggregation Gen is simple. Simply install the NuGet package into your [Api project](https://github.com/sajad-mirzaei/ZaminSample1) and follow these steps:

**If we have an aggregate with "Users" names and two properties p1 and p2::**

1- The expression "//SqlCommandsCommandDbContextDbSet" and "//SqlCommandsCommandDbContextUsing" add to the ProjectNameCommandDbContext file as follows:
```C#
//SqlCommandsCommandDbContextUsing

namespace ProjectName.Infra.Data.Sql.Commands.Common
{
	 public class ProjectNameCommandDbContext : BaseOutboxCommandDbContext
	 {
//SqlCommandsCommandDbContextDbSet
		 public ProjectNameCommandDbContext(DbContextOptions<ProjectNameCommandDbContext> options) : base(options)
		 {
		 }
		 protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		 {
			 base.OnConfiguring(optionsBuilder);
		 }
		 protected override void OnModelCreating(ModelBuilder builder)
		 {
		 }
	 }
}
```

1- The expression "//SqlQueriesQueryDbContextDbSet" add to the ProjectNameQueryDbContext file as follows:
```C#
namespace ProjectName.Infra.Data.Sql.Queries.Common
{
	public partial class ProjectNameQueryDbContext : BaseQueryDbContext
	{
		public ProjectNameQueryDbContext(DbContextOptions<ProjectNameQueryDbContext> options)
			: base(options)
		{
		}

//SqlQueriesQueryDbContextDbSet

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

		}
	}
}
```

3- use as follows:
```C#
AggregateGeneratorModel oAggregateGeneratorModel = new AggregateGeneratorModel()
{
	AggregatePlural = "Users",
	AggregateName = "User",
	ProjectName = "ProjectName",
	ProjectPath = "ProjectPath",
	AggregateClass = "class User {\n    public int P1 { get; set; }\n    public string P2 { get; set; }\n}",
	
	//Optional, if your CommandDbContextPath path is different.
	CommandDbContextPath = "ProjectPath\\2.Infra\\Data\\ProjectName.Infra.Data.Sql.Commands\\Common\\ProjectNameCommandDbContext.cs",
	
	//Optional, if your QueryDbContextPath path is different.
	QueryDbContextPath = "ProjectPath\\2.Infra\\Data\\ProjectName.Infra.Data.Sql.Queries\\Common\\ProjectNameQueryDbContext.cs"
};
AggregateGenerator oAggregateGenerator = new(oAggregateGeneratorModel);
oAggregateGenerator.Generate();
```

## Project Structure ([Api Sample](https://github.com/sajad-mirzaei/ZaminSample1)):

This repository contains a Class Library project that is designed to work within the following directory structure:
```Tree
├───1.Core
│   ├───ProjectName.Core.ApplicationServices
│   │   └───Users (AggregatePlural)
│   │       ├───Commands
│   │       │   └───CreateUser (CreateAggregateName)
│   │       └───Queries
│   │           ├───GetUserById (GetAggregateNameById)
│   │           └───GetUsers (GetAggregatePlural)
│   ├───ProjectName.Core.Contracts
│   │   └───Users (AggregatePlural)
│   │       ├───Commands
│   │       │   └───CreateUser (CreateAggregateName)
│   │       └───Queries
│   │           ├───GetUserById (GetAggregateNameById)
│   │           └───GetUsers (GetAggregatePlural)
│   └───ProjectName.Core.Domain
│       └───Users (AggregatePlural)
│           ├───Entities
│           └───Events
├───2.Infra
│   └───Data
│       ├───ProjectName.Infra.Data.Sql.Commands
│       │   └───Users (AggregatePlural)
│       │       └───Configs
│       └───ProjectName.Infra.Data.Sql.Queries
│           └───Users (AggregatePlural)
└───3.Endpoints
    └───ProjectName.Endpoints.API
        └───Users (AggregatePlural)
```

This structure is essential for the proper functioning of the Class Library.

## Entity generator configs, change this files:
1- Core.Contracts/AggregatePlural/Queries/IAggregateNameQueryRepository.cs

	1-1- Add "//EntityIQueryRepositoryUsingReplacementText" before namespace of IAggregateNameQueryRepository.cs
 	1-2- Add "//EntityIQueryRepositoryReplacementText" to end of your IAggregateNameQueryRepository.cs 
example:
```
//EntityIQueryRepositoryUsingReplacementText

namespace ProjectName.Core.Contracts.AggregatePlural.Queries;
public interface IAggregateNameQueryRepository : IQueryRepository
{

//EntityIQueryRepositoryReplacementText
}
```


2- Core.Domain/AggregatePlural/Entities/AggregateName.cs

	2-1- Add "//EntityPropertiesReplacementText" to region Properties
	2-2- Add "//EntityMethodsReplacementText" end of your AggregateName.cs
example:
```
public class AggregateName : AggregateRoot<IdTypeReplacement>
{
    #region Properties

//EntityPropertiesReplacementText
    #endregion

//EntityMethodsReplacementText
}
```

3- Endpoints/AggregatePlural/AggregateNameController.cs

	3-1- Add "//EntityControllerUsingReplacementText" before namespace of AggregateNameController.cs
	3-2- Add "//EntityControllerMethodsReplacementText" to end of your AggregateNameController.cs 
example:
```
//EntityControllerUsingReplacementText

namespace ProjectName.Endpoints.API.AggregatePlural;

[Route(""api/[controller]"")]
public class AggregateNameController : BaseController
{

//EntityControllerMethodsReplacementText
}
```

4- Sql.Commands/AggregatePlural/Configs/AggregateNameConfig.cs

	4-1- Add "//EntityCommandConfigReplacementText" to end of your AggregateNameConfig.cs
example:
```
public class AggregateNameConfig : IEntityTypeConfiguration<AggregateName>
{
	public void Configure(EntityTypeBuilder<AggregateName> builder)
	{

//EntityCommandConfigReplacementText
	}
}
```

5- Sql.Queries/AggregatePlural/AggregateNameQueryRepository.cs

	5-1- Add "//EntityControllerUsingReplacementText" before namespace of AggregateNameQueryRepository.cs
	5-2- Add "//EntityControllerMethodsReplacementText" to end of your AggregateNameQueryRepository.cs 
example:
```
//EntityQueryRepositoryUsingReplacementText

namespace ProjectName.Infra.Data.Sql.Queries.AggregatePlural;

public class AggregateNameQueryRepository : BaseQueryRepository<ProjectNameQueryDbContext>,
    IAggregateNameQueryRepository
{

//EntityQueryRepositoryReplacementText
}
```
