# Lab 5

## Dependency Injection and Service Lifetimes

### Objectives

Explaining the difference between the different service lifetimes and how to use them in a .NET 8 application.

# Transient

## A new instance of the service is created for each request.

# Scoped

## A new instance of the service is created for each scope.

# Singleton

## A single instance of the service is created for the lifetime of the application. This is the default lifetime.

### Instructions

1. Create a new folder called `Services` in the root of the project.
2. Create a new interface called `ISingletonService` in the `Services` folder.
3. Create a new interface called `IScopedService` in the `Services` folder.
4. Create a new interface called `ITransientService` in the `Services` folder.
5. All 3 interfaces will have the same method signature `string GetGuid()`.
   ```csharp
    public interface I<nameofI>Service
    {
        string GetGuid();
    }
   ```
6. Create 3 classes in the `Services` folder that implement the 3 interfaces.
7. The `GetGuid` method will return a new Guid.
8. ```csharp
    public class NameofCService : INameofIService
    {
        private readonly Guid Id;
        public NameofCService()
        {
            Id = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Id.ToString();
        }
    }
    ```
9- Register the 3 services in the `program.cs` file.
    ```csharp
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();
    builder.Services.AddSingleton<ISingletonGuidService, SingletoneGuidService>();
    builder.Services.AddTransient<ITransientGuidService, TransientGuidService>();
    builder.Services.AddScoped<IScopedGuidService, ScopedGuidService>();
    ```
10. In the HomeController, inject the 3 services and call the `GetGuid` method on each service.
  ```csharp
    public class HomeController : Controller
    {
        private readonly IScopedService _scopedService1;
        private readonly IScopedService _scopedService2;

        private readonly ITransientService _transientService1;
        private readonly ITransientService _transientService2;

        private readonly ISingletonService _singletonService1;
        private readonly ISingletonService _singletonService2;


        private readonly ILogger<HomeController> _logger;

        public HomeController(IScopedService scopedguid1, IScopedService scopeguid2,
                            ISingletonService single1, ISingletonService single2,
                            ITransientService trans1, ITransientService trans2)
        {
            _scopedService1 = scopedguid1;
            _scopedService2 = scopeguid2;
            _transientService1 = trans1;
            _transientService2 = trans2;
            _singletonService1 = single1;
            _singletonService2 = single2;
        }

        public IActionResult Index()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Transient 1 : {_transientService1.GetGuid()}\n");
            sb.Append($"Transient 2 : {_transientService2.GetGuid()}\n\n\n");
            sb.Append($"Scoped 1 : {_scopedService1.GetGuid()}\n");
            sb.Append($"Scoped 2 : {_scopedService2.GetGuid()}\n\n\n");
            sb.Append($"Singleton 1 : {_singletonService1.GetGuid()}\n");
            sb.Append($"Singleton 2 : {_singletonService2.GetGuid()}\n\n\n");

            return Ok(sb.ToString());
        }
    }
  ```

  11. Run the application and navigate to the home page. You should see the different Guids for each service lifetime.
