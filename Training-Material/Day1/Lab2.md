# Lab 2

## 1- Adding the Category Model Class
```C#
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}
```

## 2- Make sure the SQL Server Management Studio is running to validate the next steps of creating a database using Migrations

## 3- Add the connection string to the appsettings.json file
```JSON
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\sql2022;Database=testADA;Trusted_Connection=True;TrustServerCertificate=True;"
    }
}
```

## 4- install the following Nuget packages:

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools

## 5- Create the ApplicationDbContext Class
```C#
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
	}
}
```

## 6- Add the DB service to the Program.cs file (Dependency Injection)

```C#
builder.Services.AddDbContext<ApplicationDBContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

## 7- Create the Database using the migration from the Package Manager Console

```
update-database
```

## 8- Now let's add the Category table in a new migration

```C#
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {
        }

        public DbSet<Ada.Web.Models.Category> Categories { get; set; }
    }
```
### Head back to the Package Manager Console and add a migration for the new Category table

```
add-migrations AddCategoryTabletoDB
```
> [!IMPORTANT]
The migration file will be created in the Data/Migrations folder, take a look and see the code generated for the new table.