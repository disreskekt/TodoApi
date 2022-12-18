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
            .HasData(
                new Todo
                {
                    Id = 1,
                    Header = "Create a ticket",
                    Category = Categories.Analytics,
                    Color = Colors.Red,
                    CreationDate = DateTime.Now,
                    IsDone = false
                },
                new Todo
                {
                    Id = 2,
                    Header = "Request information",
                    Category = Categories.Bookkeeping,
                    Color = Colors.Green,
                    CreationDate = DateTime.Now,
                    IsDone = false
                });
        modelBuilder.Entity<Comment>()
            .HasData(
                new Comment
                {
                    Id = 1,
                    TodoId = 1,
                    Text = "First comment"
                },
                new Comment
                {
                    Id = 2,
                    TodoId = 1,
                    Text = "Second comment"
                },
                new Comment
                {
                    Id = 3,
                    TodoId = 1,
                    Text = "Third comment"
                });
    }
}