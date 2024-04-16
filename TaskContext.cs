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
        modelBuilder.Entity<Category>(c =>
        {
            c.ToTable("Category");
            c.HasKey(p => p.CategoryId);
            c.Property(p => p.Name).IsRequired().HasMaxLength(150);
            c.Property(p => p.Description);
        });
        modelBuilder.Entity<Task>(task=>
        {
            task.ToTable("Task");
            task.HasKey(p=>p.TaskId);
            //This is the relation that contains both, category and task
            task.HasOne(p=>p.Category).WithMany(p=>p.Tasks).HasForeignKey("CategoryId");
            task.Property(p=>p.Title).IsRequired().HasMaxLength(200);
            task.Property(p=>p.Description);
            task.Property(p=>p.TaskPriority);
            task.Property(p=>p.CreationDate);
            task.Ignore(p=>p.Summary);
        });
    }
}