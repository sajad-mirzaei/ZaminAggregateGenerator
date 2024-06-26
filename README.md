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
│   │   └───Users
│   │       ├───Commands
│   │       │   └───CreateUser
│   │       └───Queries
│   │           ├───GetUserById
│   │           └───GetUsers
│   ├───ProjectName.Core.Contracts
│   │   └───Users
│   │       ├───Commands
│   │       │   └───CreateUser
│   │       └───Queries
│   │           ├───GetUserById
│   │           └───GetUsers
│   └───ProjectName.Core.Domain
│       └───Users
│           ├───Entities
│           └───Events
├───2.Infra
│   └───Data
│       ├───ProjectName.Infra.Data.Sql.Commands
│       │   └───Users
│       │       └───Configs
│       └───ProjectName.Infra.Data.Sql.Queries
│           └───Users
└───3.Endpoints
    └───ProjectName.Endpoints.API
        └───Users
```

This structure is essential for the proper functioning of the Class Library.
