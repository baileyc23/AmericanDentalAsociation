# Lab 6

## Server Side Validation

### 1- Add MaxLength and Range to the Model

```csharp
public class Category
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    [DisplayName("Category Name")]
    public string? Name { get; set; }
    [DisplayName("Display Order")]
    [Range(1,100)]
    public int DisplayOrder { get; set; }
}
```

### 2- Add Server Side Validation to the Controller

```csharp
[HttpPost]
public IActionResult Create(Category obj)
{
    if (ModelState.IsValid)
    {
        _db.Categories.Add(obj);
        _db.SaveChanges();
    }          
    return RedirectToAction("Index");
}
```
### 3- now let's try to return to the view itself in case of an error

```csharp
[HttpPost]
public IActionResult Create(Category obj)
{
	if (ModelState.IsValid)
	{
		_db.Categories.Add(obj);
		_db.SaveChanges();
		return RedirectToAction("Index");
	}
	return View(obj);
}
```

### 4- Show the error message in the view

```html
<div class="mb-3 row">
    @* <label>Category Name</label> *@
    <label asp-for="Name" class="p-0"></label>
    @*  <input type="text" class="form-control" /> *@
    <input asp-for="Name" class="form-control" />
    <span asp-validation-for="Name" class="text-danger"></span>
</div>
<div class="mb-3 row">
    @* <label>Display Order</label> *@
    <label asp-for="DisplayOrder" class="p-0"></label>
    @*  <input type="text" class="form-control" /> *@
    <input asp-for="DisplayOrder" class="form-control" />
    <span asp-validation-for="DisplayOrder" class="text-danger"></span>
</div>
```

### 5- Change the message of the error
```csharp
public class Category
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    [DisplayName("Category Name")]
    public string? Name { get; set; }
    [DisplayName("Display Order")]
    [Range(1,100, ErrorMessage ="Display Order needs to be between 1 and 100")]
    public int DisplayOrder { get; set; }
}
```

### 6- Add a custom Validation (Name and Display Order can not be the same value for example)

```csharp
[HttpPost]
public IActionResult Create(Category obj)
{
    if (obj.Name == obj.DisplayOrder.ToString())
    {
        ModelState.AddModelError("Name", "The Name and Display Order fields cannot be the same");
    }
            
    if (ModelState.IsValid)
    {
        _db.Categories.Add(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }          
    return View();
}
```
### 7- Add a Validation Summary to the View

```html
<div class=" row pb-2">
    <h2 class="text-primary">Create New Category</h2>
    <hr/>
</div>
<div asp-validation-summary="All"></div>
<div class="mb-3 row">
    @* <label>Category Name</label> *@
    <label asp-for="Name" class="p-0"></label>
    @*  <input type="text" class="form-control" /> *@
    <input asp-for="Name" class="form-control" />
    <span asp-validation-for="Name" class="text-danger"></span>
</div>
```

### 8- Add a second custom model validation

```csharp
[HttpPost]
public IActionResult Create(Category obj)
{
    if (obj.Name == obj.DisplayOrder.ToString())
    {
        ModelState.AddModelError("Name", "The Name and Display Order fields cannot be the same");
    }
    if(obj.Name.ToLower() == "test")
    {                 
        ModelState.AddModelError("", "The Name field cannot be 'test'");
    }
            
    if (ModelState.IsValid)
    {
        _db.Categories.Add(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }          
    return View();
}
```

### 9- Now try the Validation summary with ModelOnly and None and see what happens

```html
<div class=" row pb-2">
    <h2 class="text-primary">Create New Category</h2>
    <hr/>
</div>
<div asp-validation-summary="ModelOnly"></div>
<div class="mb-3 row">
    @* <label>Category Name</label> *@
    <label asp-for="Name" class="p-0"></label>
    @*  <input type="text" class="form-control" /> *@
    <input asp-for="Name" class="form-control" />
    <span asp-validation-for="Name" class="text-danger"></span>
</div>
```
### 10- Now let's try the validation Client Side

Take a look at the _ValidationScriptsPartial.cshtml under the Shared folder in Views.
It has to be added to the Create.cshtml view
```html
@section Scripts {
    @{
        <partial name ="_ValidationScriptsPartial" />
    }
}
```
