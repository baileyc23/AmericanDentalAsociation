# Lab 11

## 1- Add a new model and call it "Course"

```csharp
public class Course
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Title { get; set; }
    public string? Description { get; set; }
    [Required]
    public string? Instructor { get; set; }
    [Display(Name = "Price per person for 1 to 3 attendees")]
    public double? Price { get; set; }
    [Display(Name = "Price per person for 4 or more attendees")]
    public double? Price4 { get; set; }
    [Display(Name = "Price for a private class with a maximum of 10 attendees")]
    public double? PricePrivate { get; set; }
    public bool? Online { get; set; }
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    [ValidateNever]
    public Category Category { get; set; }
    [ValidateNever]
    public string ImageUrl { get; set; }    
}
```

## 2- Add a new controller and call it "CourseController"

```csharp
public class CourseController : Controller
{
    private readonly IUnitOfWork _unitofwork;
    public CourseController(IUnitOfWork unitofwork)
    {
        _unitofwork = unitofwork;
    }
    public IActionResult Index()
    {
        List<Course> CourseList = _unitofwork.CourseRepository.GetAll().ToList();            
        return View(CourseList);
    }

    public IActionResult Create()
    {
        //Projections in EFCore
        //IEnumerable<SelectListItem> CategoryList = _unitofwork.CategoryRepository.GetAll().Select(i => new SelectListItem
        //{
        //    Text = i.Name,
        //    Value = i.Id.ToString()
        //});

        //ViewBag.CategoryList = CategoryList;

        //New Way using ViewModel
        CourseVM courseVM = new CourseVM()
        {
            Course = new Course(),
            CategoryList = _unitofwork.CategoryRepository.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            })
        };

        return View(courseVM);
    }

    [HttpPost]
    public IActionResult Create(CourseVM obj)
    {
        if (obj !=null && obj.Course.Title == obj.Course.Description.ToString())
        {
            ModelState.AddModelError("Ttile", "The Ttile and Description fields cannot be the same");
        }
        if (obj != null && obj.Course.Title.ToLower() == "test")
        {
            ModelState.AddModelError("", "The Title field cannot be 'test'");
        }

        if (ModelState.IsValid)
        {
            _unitofwork.CourseRepository.Add(obj.Course);
            _unitofwork.Save();
            TempData["success"] = "Course created successfully";
            return RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Course? courseFromDB = _unitofwork.CourseRepository.Get(u => u.Id == id);

        // Other options to find the category
        //Category categoryFromDB = _db.Categories.FirstOrDefault(u => u.Id == id);
        //Category categoryFromDB = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

        if (courseFromDB == null)
        {
            return NotFound();
        }
        return View(courseFromDB);
    }

    [HttpPost]
    public IActionResult Edit(CourseVM obj)
    {
        if (ModelState.IsValid)
        {
            _unitofwork.CourseRepository.Update(obj.Course);
            _unitofwork.Save();
            TempData["success"] = "Course updated successfully";
            return RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Course? courseFromDB = _unitofwork.CourseRepository.Get(u => u.Id == id);

        // Other options to find the category
        //Category categoryFromDB = _db.Categories.FirstOrDefault(u => u.Id == id);
        //Category categoryFromDB = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

        if (courseFromDB == null)
        {
            return NotFound();
        }
        return View(courseFromDB);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Course? obj = _unitofwork.CourseRepository.Get(u => u.Id == id);
        if (obj == null)
        {
            return NotFound();
        }
        _unitofwork.CourseRepository.Remove(obj);
        _unitofwork.Save();
        TempData["success"] = "Course deleted successfully";
        return RedirectToAction("Index");
    }
}
```

## 3- Add a new view and call it "Index.cshtml"

```html
@model List<Course>

<div class="container">
    <div class="row pt-4 pb-3">
        <div class="col-6">
            <h2 class="text-primary">Course List</h2>
        </div>
        <div class="col-6 text-end">
            <a asp-controller="Course" asp-action="Create" class="btn btn-primary"> <i class="bi bi-plus-circle"></i>Create New Course</a>
        </div>
    </div>
<table class="table table-bordered table-striped">
    <thead>
        <tr>
            <th>Course Name</th>
            <th>Description</th>
            <th>Instructor</th>
            <th>Price</th>
            <th>Price for 4 or more</th>
            <th>Price for Private Class</th>
            <th>Online</th>
            <th></th>
        </tr>
    </thead>
    <tbody>
            @foreach (var course in Model)
            {

                <tr>
                    <td>@course.Title</td>
                    <td>@course.Description</td>
                    <td>@course.Instructor</td>
                    <td>@course.Price</td>
                    <td>@course.Price4</td>
                    <td>@course.PricePrivate</td>
                    <td>@course.Online</td>
                    <td>
                        <div class="w-75 btn-group" role="group">
                            <a asp-controller="Course" asp-action="Edit" asp-route-id="@course.Id" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
                            <a asp-controller="Course" asp-action="Delete" asp-route-id="@course.Id" class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i>Delete</a>
                        </div>
                    </td>
                </tr>
            }
     </tbody>
</table>
</div>
```

## 4- Add a new view and call it "Create.cshtml"

```html
@model CourseVM

<form method="post">
    <div class="border p-3 mt-4">
        <div class=" row pb-2">
            <h2 class="text-primary">Create New Course</h2>
            <hr/>
        </div>
        <div asp-validation-summary="All"></div>
        <div class="mb-3 row">
            @* <label>Category Name</label> *@
            <label asp-for="Course.Title" class="p-0"></label>
           @*  <input type="text" class="form-control" /> *@
            <input asp-for="Course.Title" class="form-control" />
            <span asp-validation-for="Course.Title" class="text-danger"></span>
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="Course.Description" class="p-0"></label>
           @*  <input type="text" class="form-control" /> *@
            <textarea asp-for="Course.Description" class="form-control"></textarea>
            <span asp-validation-for="Course.Description" class="text-danger"></span>
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="Course.Instructor" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="Course.Instructor" class="form-control" />
            <span asp-validation-for="Course.Instructor" class="text-danger"></span>
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="Course.Price" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="Course.Price" class="form-control" />
            <span asp-validation-for="Course.Price" class="text-danger"></span>
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="Course.Price4" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="Course.Price4" class="form-control" />
            <span asp-validation-for="Course.Price4" class="text-danger"></span>
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="Course.PricePrivate" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="Course.PricePrivate" class="form-control" />
            <span asp-validation-for="Course.PricePrivate" class="text-danger"></span>
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="Course.Online" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="Course.Online" class="form-control" />
            <span asp-validation-for="Course.Online" class="text-danger"></span>
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="Course.CategoryId" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <select asp-for="@Model.Course.CategoryId" asp-items="@Model.CategoryList" class="form-select">
                <option disabled selected>--Select Category--</option>
            </select>
            <span asp-validation-for="Course.CategoryId" class="text-danger"></span>
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="Course.ImageUrl" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="Course.ImageUrl" class="form-control" />
            <span asp-validation-for="Course.ImageUrl" class="text-danger"></span>
        </div>
        <div class="row">
            <div class="col-6 col-md-3">
                <button type="submit" class="btn btn-primary form-control">Create</button>
            </div>
            <div class="col-6 col-md-3">
                <a asp-controller="Course" asp-action="Index" class="btn btn-outline-secondary form-control border">Back to List</a>
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

## 5- Add a new view and call it "Edit.cshtml"

```html
@model Course

<form method="post">
    @* <input asp-for="Id" hidden /> *@
    <div class="border p-3 mt-4">
        <div class=" row pb-2">
            <h2 class="text-primary">Edit Category</h2>
            <hr/>
        </div>
        <div asp-validation-summary="All"></div>
        <div class="mb-3 row">
            @* <label>Category Name</label> *@
            <label asp-for="Title" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="Title" class="form-control" />
            <span asp-validation-for="Title" class="text-danger"></span>
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="Description" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="Description" class="form-control" />
            <span asp-validation-for="Description" class="text-danger"></span>
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="Instructor" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="Instructor" class="form-control" />
            <span asp-validation-for="Instructor" class="text-danger"></span>
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="Price" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="Price" class="form-control" />
            <span asp-validation-for="Price" class="text-danger"></span>
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="Price4" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="Price4" class="form-control" />
            <span asp-validation-for="Price4" class="text-danger"></span>
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="PricePrivate" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="PricePrivate" class="form-control" />
            <span asp-validation-for="PricePrivate" class="text-danger"></span>
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="Online" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="Online" class="form-control" />
            <span asp-validation-for="Online" class="text-danger"></span>
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

## 6- Add a new view and call it "Delete.cshtml"

```html
@model Course

<form method="post">
    <input asp-for="Id" hidden />
    <div class="border p-3 mt-4">
        <div class=" row pb-2">
            <h2 class="text-primary">Delete Course</h2>
            <hr />
        </div>
        <div class="mb-3 row">
            @* <label>Category Name</label> *@
            <label asp-for="Title" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="Title" class="form-control" />
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="Description" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="Description" class="form-control" />
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="Instructor" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="Instructor" class="form-control" />
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="Price" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="Price" class="form-control" />
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="Price4" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="Price4" class="form-control" />
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="PricePrivate" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="PricePrivate" class="form-control" />
        </div>
        <div class="mb-3 row">
            @* <label>Display Order</label> *@
            <label asp-for="Online" class="p-0"></label>
            @*  <input type="text" class="form-control" /> *@
            <input asp-for="Online" class="form-control" />
        </div>
        
        <div class="row">
            <div class="col-6 col-md-3">
                <button type="submit" class="btn btn-danger form-control">Delete</button>
            </div>
            <div class="col-6 col-md-3">
                <a asp-controller="Course" asp-action="Index" class="btn btn-outline-secondary form-control border">Back to List</a>
            </div>
        </div>
    </div>

</form>
```

## 7- Add a new view model and call it "CourseVM"

```csharp
public class CourseVM
{
    public Course Course { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem> CategoryList { get; set; }
}
```

