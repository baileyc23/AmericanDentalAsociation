# Lab 7

### 1- Add the Edit and Delete buttons to the Table on the Index.cshtml view
```html
<table class="table table-bordered table-striped">
    <thead>
        <tr>
            <th>Category Name</th>
            <th>Display Order</th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        @foreach (var category in Model)
        {
        
                <tr>
                    <td>@category.Name</td>
                    <td>@category.DisplayOrder</td>
                    <td>
                        <div class="w-75 btn-group" role="group">
                            <a asp-controller="Category" asp-action="Edit" asp-route-id="@category.Id" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
                            <a asp-controller="Category" asp-action="Delete" asp-route-id="@category.Id" class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i>Delete</a>
                        </div>
                    </td>
                 </tr>
        }
     </tbody>
</table>
```

### 2- Add the Edit functionality to the controller

```csharp
public IActionResult Edit(int? id)
{
    if (id == null || id == 0)
    {
        return NotFound();
    }
    Category? categoryFromDB = _db.Categories.Find(id);
            
    // Other options to find the category
    //Category categoryFromDB = _db.Categories.FirstOrDefault(u => u.Id == id);
    //Category categoryFromDB = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

    if (categoryFromDB == null)
    {
        return NotFound();
    }
    return View(categoryFromDB);
}
```

### 3- Add the Edit POST method to the controller

```csharp
[HttpPost]
public IActionResult Edit(Category obj)
{
    if (ModelState.IsValid)
    {
        _db.Categories.Update(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    return View();
}
```

### 4- Add the Edit View (identical to the Create View except Changing the a couple of values from create to Update)
```html
@model Category

<form method="post">
    <div class="border p-3 mt-4">
        <div class=" row pb-2">
            <h2 class="text-primary">Edit Category</h2>
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
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="DisplayOrder" class="p-0"></label>
           @*  <input type="text" class="form-control" /> *@
            <input asp-for="DisplayOrder" class="form-control" />
            <span asp-validation-for="DisplayOrder" class="text-danger"></span>
        </div>
        <div class="row">
            <div class="col-6 col-md-3">
                <button type="submit" class="btn btn-primary form-control">Update</button>
            </div>
            <div class="col-6 col-md-3">
                <a asp-controller="Category" asp-action="Index" class="btn btn-outline-secondary form-control border">Back to List</a>
            </div>
        </div>   
    </div>

</form>

@section Scripts {
    @{
        <partial name ="_ValidationScriptsPartial" />
    }
}
```

### 5- Explain the the hidden Id input in the Edit View
```html 
<form method="post">
    @* <input asp-for="Id" hidden /> *@
    <div class="border p-3 mt-4">
        <div class=" row pb-2">
            <h2 class="text-primary">Edit Category</h2>
            <hr/>
        </div>
```

### 6- Add the Delete functionality to the controller
```csharp
 public IActionResult Delete(int? id)
{
    if (id == null || id == 0)
    {
        return NotFound();
    }
    Category? categoryFromDB = _db.Categories.Find(id);

    // Other options to find the category
    //Category categoryFromDB = _db.Categories.FirstOrDefault(u => u.Id == id);
    //Category categoryFromDB = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

    if (categoryFromDB == null)
    {
        return NotFound();
    }
    return View(categoryFromDB);
}
```

### 7- Add the Delete POST method to the controller
```csharp
[HttpPost, ActionName("Delete")]
public IActionResult DeletePost(int? id)
{
    Category? obj = _db.Categories.Find(id);
    if (obj == null)
    {
        return NotFound();
    }
    _db.Categories.Remove(obj);
    _db.SaveChanges();
    return RedirectToAction("Index");
}
```

### 8- Add the Delete View
```html
@model Category

<form method="post">
    <input asp-for="Id" hidden />
    <div class="border p-3 mt-4">
        <div class=" row pb-2">
            <h2 class="text-primary">Delete Category</h2>
            <hr />
        </div>
        <div class="mb-3 row">
            @* <label>Category Name</label> *@
            <label asp-for="Name" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="Name" disabled class="form-control" />           
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="DisplayOrder" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="DisplayOrder" disabled class="form-control" />
        </div>
        <div class="row">
            <div class="col-6 col-md-3">
                <button type="submit" class="btn btn-danger form-control">Delete</button>
            </div>
            <div class="col-6 col-md-3">
                <a asp-controller="Category" asp-action="Index" class="btn btn-outline-secondary form-control border">Back to List</a>
            </div>
        </div>
    </div>

</form>
```
