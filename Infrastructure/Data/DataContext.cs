using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data;

public class DataContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>()
            .HasIndex(todo => new
            {
                todo.Category,
                todo.Header
            })
            .IsUnique();
        
        modelBuilder.Entity<Todo>()
            .Property(todo => todo.Color)
            .HasConversion(new EnumToStringConverter<Colors>());
        
        modelBuilder.Entity<Todo>()
            .HasData(new Todo
                {
                    Header = "Create a ticket",
                    Category = Categories.Analytics,
                    Color = Colors.Red,
                    CreationDate = DateTime.Now,
                    IsDone = false,
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            Text = "First comment"
                        },
                        new Comment
                        {
                            Text = "Second comment"
                        },
                        new Comment
                        {
                            Text = "Third comment"
                        }
                    }
                },
                new Todo
                {
                    Header = "Request information",
                    Category = Categories.Bookkeeping,
                    Color = Colors.Green,
                    CreationDate = DateTime.Now,
                    IsDone = false
                });
    }
}