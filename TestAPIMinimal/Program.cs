using Microsoft.EntityFrameworkCore;
using TestAPIMinimal.Data;
using TestAPIMinimal.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CategoryContext>(opt => opt.UseInMemoryDatabase("CategoryList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/categoryitems", async (CategoryContext db) =>
    await db.Categories.ToListAsync());

app.MapGet("/categoryitems/{id}", async (int id, CategoryContext db) =>
    await db.Categories.FindAsync(id)
        is Category category
            ? Results.Ok(category)
            : Results.NotFound());

app.MapPost("/categoryitems", async (Category category, CategoryContext db) =>
{
    db.Categories.Add(category);
    await db.SaveChangesAsync();

    return Results.Created($"/categoryitems/{category.Id}", category);
});

app.MapPut("/categoryitems/{id}", async (int id, Category inputcategory, CategoryContext db) =>
{
    var category = await db.Categories.FindAsync(id);

    if (category is null) return Results.NotFound();

    category.Name = inputcategory.Name;
    category.DisplayOrder = inputcategory.DisplayOrder;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/categoryitems/{id}", async (int id, CategoryContext db) =>
{
    if (await db.Categories.FindAsync(id) is Category category)
    {
        db.Categories.Remove(category);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();
