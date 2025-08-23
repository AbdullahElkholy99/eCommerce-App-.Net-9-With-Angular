using Ecom.Core.Entity.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Data.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder.HasData(
            new Category { Id = 1, Name = "Electronics", Description = "Electronic gadgets and devices" },
            new Category { Id = 2, Name = "Phone", Description = "Phone Smart" }
        );


    }
}

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired();

        builder.Property(x => x.Price)
            .HasColumnType("decimal(18,2)");

        builder.HasData(
         new Product { Id = 1, Name = "Iphone", Description = "iphone made by Apple" , CategoryId =2, Price = 5000.50m },
         new Product { Id = 2, Name = "TV", Description = "TV made by Samsung", CategoryId =1, Price = 1000.50m }
        );


    }
}
