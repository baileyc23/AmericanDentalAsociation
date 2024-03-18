# Lab 3

## 1- Add a new Category Controller

## 2 - Add an Index View to show the ActionResult of the Category Controller under the View folder and the Category folder
### Place a a simple HTML H1 tage displaying the Category list
```html
  <h1>Category List</h1>
```

## 3- Run the app and head over to the route /Category to see the new view

## 4- Add the Category link to the Navigation menu by adding it to the _Layout.cshtml file under "Shared" folder

```html
	<li class="nav-item">
		<a class="nav-link text-dark" asp-area="" asp-controller="Category" asp-action="Index">Category</a>
	</li>
```

## 5- Inject the ApplicationDbContext into the Category Controller and return the list of Categories to the Index View

```csharp
public class CategoryController : Controller
{
    private readonly ApplicationDBContext _db;
    public CategoryController(ApplicationDBContext db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        List<Category> CategoryList = _db.Categories.ToList();
        return View(CategoryList);
    }
}
```

## 6- Add to the view an iterator to display the list of Categories

```html
@model List<Category>

<h1>Category List</h1>

<table class="table table-bordered table-striped">
    <thead>
        <tr>
            <th>Category Name</th>
            <th>Display Order</th>
        </tr>
    </thead>
    @foreach (var category in Model)
    {
        <tbody>
            <tr>
                <td>@category.Name</td>
                <td>@category.DisplayOrder</td>
             </tr>
        </tbody>
    }
</table>
```

## 7- Seed the database with some Categories

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Category>().HasData(
                        new Category { Id = 1, Name = "Azure", DisplayOrder = 1 },
                        new Category { Id = 2, Name = "AWS", DisplayOrder = 2 },
                        new Category { Id = 3, Name = "Sitefinity", DisplayOrder = 3 },
                        new Category { Id = 4, Name = "Generative AI", DisplayOrder = 4 },
                        new Category { Id = 5, Name = "Terraform", DisplayOrder = 5 },
                        new Category { Id = 6, Name = "Databricks", DisplayOrder = 6 }
                        );
}
```