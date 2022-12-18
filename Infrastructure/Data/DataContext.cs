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
    }
}