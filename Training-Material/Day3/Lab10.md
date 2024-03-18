# Lab 10

## IRepositoy Pattern

### Objectives
Learning how to use the IRepository pattern to abstract the data access layer.

### Instructions

1. Create a new folder called `Repository` in the `DataAccessLayer` project
2. Create a new interface called `IRepository` in the `Repository` folder
```csharp
public interface IRepository<T> where T : class
{
    // T will be category, product, etc.
    IEnumerable<T> GetAll();
    //T Get(int id);
    T Get(Expression<Func<T, bool>> filter);
    void Add(T entity);
    //void Update(T entity); I would like to keep update outside of the IRepository as it is different based on the entity most of the cases.
    //void Delete(T entity); I usually don't like to call it delete, I prefer to call it remove.
    void Remove(T entity);  
    void RemoveRange(IEnumerable<T> entity);
}
```
3. Create a new class called `Repository` in the `Repository` folder
```csharp
public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDBContext _db;
    internal DbSet<T> dbSet;
    public Repository(ApplicationDBContext db)
    {
        _db = db;
        this.dbSet = _db.Set<T>();
    }
    public void Add(T entity)
    {
        // I can't do _db.Categories.Add because T does not know it is Categories yet.
        dbSet.Add(entity);
    }

    public T Get(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        return query.FirstOrDefault();

    }

    public IEnumerable<T> GetAll()
    {
        IQueryable<T> query = dbSet;
        return query.ToList();
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entity)
    {
       dbSet.RemoveRange(entity);
    }
}
```
4. Explanation of why we used the dbSet instead of _db.Categories
5. Create a new interface called `ICategoryRepository` in the `Repository` folder
```csharp
public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category obj);
    void Save();
}
```

6. Create a new class called `CategoryRepository` in the `Repository` folder
```csharp
public  class CategoryRepository : Repository<Category>, ICategoryRepository 
{
    private ApplicationDBContext _db;
    public CategoryRepository(ApplicationDBContext db) : base(db)
    {
        _db = db;
    }
    
    public void Save()
    {
        _db.SaveChanges();
    }

    public void Update(Category obj)
    {
        _db.Categories.Update(obj);
    }
}
```
7. Why did we have to do the injection of the `ApplicationDBContext` in the `CategoryRepository` class? and what heppens if we don't do it?

8. In the Controller of the Web project we will replace the _db with the _categoryRepo and inject the ICategoryRepository in the constructor of the controller. The code should look like this:
```csharp
public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepo;
    public CategoryController(ICategoryRepository db)
    {
        _categoryRepo = db;
    }
    public IActionResult Index()
    {
        List<Category> CategoryList = _categoryRepo.GetAll().ToList();
        return View(CategoryList);
    }

    public IActionResult Create()
    {
        return View();
    }

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
            _categoryRepo.Add(obj);
            _categoryRepo.Save();
            TempData["success"] = "Category created successfully";
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
        Category? categoryFromDB = _categoryRepo.Get(u=>u.Id == id);
        
        // Other options to find the category
        //Category categoryFromDB = _db.Categories.FirstOrDefault(u => u.Id == id);
        //Category categoryFromDB = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

        if (categoryFromDB == null)
        {
            return NotFound();
        }
        return View(categoryFromDB);
    }

    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (ModelState.IsValid)
        {
            _categoryRepo.Update(obj);
            _categoryRepo.Save();
            TempData["success"] = "Category updated successfully";
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
        Category? categoryFromDB = _categoryRepo.Get(u=>u.Id == id);

        // Other options to find the category
        //Category categoryFromDB = _db.Categories.FirstOrDefault(u => u.Id == id);
        //Category categoryFromDB = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

        if (categoryFromDB == null)
        {
            return NotFound();
        }
        return View(categoryFromDB);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? obj = _categoryRepo.Get(u=>u.Id == id);
        if (obj == null)
        {
            return NotFound();
        }
        _categoryRepo.Remove(obj);
        _categoryRepo.Save();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}
```

9. Run the application and navigate to the `/Category` route.  Why did it NOT work? What do we need to do to fix it?
10. Add the following code to the `program.cs` file to inject the scoped service 
```csharp
builder.Services.AddScoped<ICategoryRepository, CategoryRepository >();
```

## IUnitOfWork Pattern

### Objectives
Learning how to use the IUnitOfWork pattern to abstract the data access layer to not repeat yourself when it comes to fuunctionality that is repeated in multiple functionality. For Example the _db.SaveChanges() is repeated in the CategoryRepository and will be repeated in all other repositories that the project will have.

### Instructions




