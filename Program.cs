using efproject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using efproject.Models;
var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TasksContext>(p => p.UseInMemoryDatabase("TasksDB"));
builder.Services.AddSqlServer<TasksContext>(builder.Configuration.GetConnectionString("SqlServerConnection"));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconnection", async ([FromServices] TasksContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok($"In memory DB: {dbContext.Database.IsSqlServer()}");
});

app.MapGet("/api/tasks", async ([FromServices] TasksContext dbContext) =>
{

    return Results.Ok(dbContext.Tasks.Include(p => p.Category).Where(p => p.TaskPriority == Priority.High));
    //return Results.Ok(dbContext.Tasks);
});
app.MapGet("/api/categories", async ([FromServices] TasksContext dbContext) =>
{
    return Results.Ok(dbContext.Categories.Where(c => c.Points > 1));
});
app.MapPost("/api/tasks", async ([FromServices] TasksContext dbContext, [FromBody] efproject.Models.Task task) =>
{
    task.TaskId = Guid.NewGuid();
    task.CreationDate = DateTime.Now;
    await dbContext.AddAsync(task);
    await dbContext.SaveChangesAsync();
    return Results.Ok();
});
app.MapPut("/api/tasks/{id}", async ([FromServices] TasksContext dbContext, [FromBody] efproject.Models.Task task, [FromRoute] Guid id) =>
{
    var currentTask = dbContext.Tasks.Find(id);

    if (currentTask != null)
    {
        currentTask.CategoryId = task.CategoryId;
        currentTask.Title = task.Title;
        currentTask.TaskPriority = task.TaskPriority;
        currentTask.Description = task.Description;
        currentTask.DueDate = task.DueDate;

        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound();
});
app.MapPut("/api/categories/{id}", async ([FromServices] TasksContext dbContext, [FromBody] efproject.Models.Category category, [FromRoute] Guid id) =>
{
    var currentCategory = dbContext.Categories.Find(id);

    if (currentCategory != null)
    {
        currentCategory.Name = category.Name;
        currentCategory.Points = category.Points;
        currentCategory.Description = category.Description;

        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound();
});

app.MapDelete("/api/tasks/{id}", async ([FromServices] TasksContext dbContext, [FromRoute] Guid id) =>
{
    var currentTask = dbContext.Tasks.Find(id);

    if (currentTask != null)
    {
        dbContext.Remove(currentTask);
        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound();
});
app.MapDelete("/api/categories/{id}", async ([FromServices] TasksContext dbContext, [FromRoute] Guid id) =>
{
    var currentCategory = dbContext.Categories.Find(id);

    if (currentCategory != null)
    {
        dbContext.Remove(currentCategory);
        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound();
});
app.Run();
