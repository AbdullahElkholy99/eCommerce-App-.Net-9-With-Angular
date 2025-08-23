using Ecom.Core.Entity.Product;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ecom.Infrastructure.Data;

//to create migration :  Add-Migration init -o "Data/Migrations"
public class AppDbContext : DbContext
{
    // Constructor to pass options to the base DbContext class 
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Photo> Photos { get; set; }


}
