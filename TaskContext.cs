namespace efproject;

using System.Formats.Tar;
using efproject.Models;
using Microsoft.EntityFrameworkCore;

public class TasksContext : DbContext
{

    public DbSet<Category> Categories { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public TasksContext(DbContextOptions<TasksContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Category> categoriesInit = new List<Category>();
        categoriesInit.Add(new Category()
        {
            CategoryId = new Guid("2f220809-95c8-40fe-8807-e466234cff54"),
            Name = "Pending activities",
            Points = 5

        });
        categoriesInit.Add(new Category()
        {
            CategoryId = new Guid("2f220809-95c8-40fe-8807-e466234cff50"),
            Name = "Personal activities",
            Points = 8

        });

        modelBuilder.Entity<Category>(c =>
        {
            c.ToTable("Category");
            c.HasKey(p => p.CategoryId);
            c.Property(p => p.Name).IsRequired().HasMaxLength(150);
            c.Property(p => p.Description).IsRequired(false);
            c.Property(p => p.Points);
            c.HasData(categoriesInit);
        });

        List<Task> tasksInit = new List<Task>();
        tasksInit.Add(new Task()
        {
            TaskId = Guid.Parse("2f220809-95c8-40fe-8807-e466234cff30"),
            CategoryId = Guid.Parse("2f220809-95c8-40fe-8807-e466234cff54"),
            TaskPriority = Priority.Medium,
            Title = "Check pending activites",
            CreationDate = DateTime.Now,
            DueDate = DateTime.Today
        });
        tasksInit.Add(new Task()
        {
            TaskId = Guid.Parse("2f220809-95c8-40fe-8807-e466234cff20"),
            CategoryId = Guid.Parse("2f220809-95c8-40fe-8807-e466234cff50"),
            TaskPriority = Priority.High,
            Title = "Check .net courses",
            CreationDate = DateTime.Now,
            DueDate = DateTime.Today
        });
        modelBuilder.Entity<Task>(task =>
        {
            task.ToTable("Task");
            task.HasKey(p => p.TaskId);
            //This is the relation that contains both, category and task
            task.HasOne(p => p.Category).WithMany(p => p.Tasks).HasForeignKey("CategoryId");
            task.Property(p => p.Title).IsRequired().HasMaxLength(200);
            task.Property(p => p.Description).IsRequired(false);
            task.Property(p => p.TaskPriority);
            task.Property(p => p.CreationDate);
            task.Property(p => p.DueDate);
            task.Ignore(p => p.Summary);
            task.HasData(tasksInit);
        });
    }
}