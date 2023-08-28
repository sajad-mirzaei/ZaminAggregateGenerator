# a Package to quickly create the files of an aggregate in the Zamin project


AggregationGenerator is a NuGet package designed to streamline and accelerate the process of creating Aggregation files across various layers of your API project. Aggregation files play a crucial role in combining related data and facilitating efficient data retrieval in modern software systems. However, generating these files manually can be time-consuming and error-prone. AggregationGenerator aims to provide a seamless solution to this challenge by automating

## Key Features:

1. **Effortless Aggregation File Creation:** With AggregationGenerator, you can effortlessly create Aggregation files that consolidate and structure your data from various sources within your API project. This reduces the need for manual file creation, saving you valuable development time.

2. **Layer-Agnostic Integration:** AggregationGenerator seamlessly integrates with all layers of your API project, whether it's the data access layer, business logic layer, or presentation layer. This cross-layer compatibility ensures that your Aggregation files are consistently generated throughout the application.

3. **Performance Boost:** By automating the Aggregation file generation process, AggregationGenerator contributes to improved project build speed. You'll experience quicker development cycles and reduced turnaround time.

## Getting Started:

Getting started with AggregationGenerator is straightforward. Simply install the NuGet package into your API project and follow the provided documentation to configure and utilize the package effectively. With AggregationGenerator, you'll experience improved code organization, enhanced data retrieval capabilities, and an optimized development workflow.
Getting started with Aggregation Gen is simple. Simply install the NuGet package into your API project and follow these steps:

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
	AggregateClass = "class User {\n    public int P1 { get; set; }\n    public string P2 { get; set; }\n}"
};
AggregateGenerator oAggregateGenerator = new(oAggregateGeneratorModel);
oAggregateGenerator.Generate();
```

## Project Structure:

This repository contains a Class Library project that is designed to work within the following directory structure:
```Tree
├───1.Core
│   ├───ProjectName.Core.ApplicationService
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
