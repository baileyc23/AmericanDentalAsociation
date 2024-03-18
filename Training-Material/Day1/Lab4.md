# Lab 4

## 1- Change the UX using a Bootswatch theme
https://bootswtach.com

## 2- Choose one of the bootswatch themes and apply it to your app

- I chose for example LUX theme
- Copy and paste the bootstrap.css code into your own lib/dist/css/Bootstrap.css file

## 3- Create a new route in your app that will display a form to add a new Category

```C#
 public IActionResult Create()
 {
     return View();
 }

```
### That will require a view as well called create.cshtml . You can use the following code to create a form:

```razor
@model Category

<form method="post">
    <div class="border p-3 mt-4">
        <div class=" row pb-2">
            <h2 class="text-primary">Create New Category</h2>
            <hr/>
        </div>
        <div class="mb-3 row">
            @* <label>Category Name</label> *@
            <label asp-for="Name" class="p-0"></label>
           @*  <input type="text" class="form-control" /> *@
           <input asp-for="Name" class="form-control" />
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="DisplayOrder" class="p-0"></label>
           @*  <input type="text" class="form-control" /> *@
            <input asp-for="DisplayOrder" class="form-control" />
        </div>
        <div class="row">
            <div class="col-6 col-md-3">
                <button type="submit" class="btn btn-primary form-control">Create</button>
            </div>
            <div class="col-6 col-md-3">
                <a asp-controller="Category" asp-action="Index" class="btn btn-outline-secondary form-control border">Back to List</a>
            </div>
        </div>     
    </div>
</form>
```

## 4- Add another route for the POST of a new Category that will submit the form to the server 

```C#
[HttpPost]
 public IActionResult Create(Category obj)
 {
     _db.Categories.Add(obj);
     _db.SaveChanges();
     return RedirectToAction("Index");
 }
```

