# Lab 12

## 1- Temp Data
Only available for the **next request**. It is useful when we want to show a message to the user after a redirect.

```csharp
public IActionResult Edit(Category obj)
{
    if (ModelState.IsValid)
    {
        _db.Categories.Update(obj);
        _db.SaveChanges();
        TempData["success"] = "Category updated successfully";
        return RedirectToAction("Index");
    }
    return View();
}
```

```html
@if (TempData["success"] != null)
{
    <h2>@TempData["success"]</h2>
}
```

## 2- Create a partial view for the Temp Data called _Notification.cshtml_

```html
@if (TempData["success"] != null)
{
    <h2>@TempData["success"]</h2>
}
@if (TempData["error"] != null)
{
    <h2>@TempData["error"]</h2>
}
```

and then on the index.cshtml view add the partial view

```html
<partial name="_Notification" />
```
## 3- Toast Notifications
Let's get fancy :) 
Head over to get toastr.js from https://github.com/CodeSeven/toastr

Change the _Notification.cshtml to use toastr

```html
@if (TempData["success"] != null)
{
    <script src="~/lib/jquery/dist/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>
    <script type="text/javascript">
        toastr.success('@TempData["success"]');
    </script>
}
@if (TempData["error"] != null)
{
    <script src="~/lib/jquery/dist/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>
    <script type="text/javascript">
        toastr.error('@TempData["error"]');
    </script>
}
```
You ### can place the partial in the index.cshtml view or in the _Layout.cshtml (_layout is preferrable)_)
```html
<div class="container">
    <main role="main" class="pb-3">
        <partial name="_Notification" />
        @RenderBody()
    </main>
</div>
```

## 4- Areas

Areas are used to organize the application into smaller functional groupings. For example, you might use an area to organize the functionality of an administration section of a website vs a regular customer/user section of a website.

### Create 2 Areas one for Admin and one for Customer from the "New Scaffolded Item" dialog in Visual Studio



## ViewBag

ViewBag is a dynamic property that takes advantage of the new dynamic features in C# 4.0. ViewBag is a wrapper around the ViewData dictionary. It allows you to create dynamic properties for the ViewBag.

```csharp
public IActionResult Create()
{
    //Projections in EFCore
    IEnumerable<SelectListItem> CategoryList = _unitofwork.CategoryRepository.GetAll().Select(i => new SelectListItem
    {
        Text = i.Name,
        Value = i.Id.ToString()
    });

    ViewBag.CategoryList = CategoryList;

    return View();
}
```



