using Ecom.Core.Entity.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Data.Configuration;

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

        builder.Property(x => x.NewPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.OldPrice)
            .HasColumnType("decimal(18,2)");

        builder.HasData(
         new Product { Id = 1, Name = "Iphone", Description = "iphone made by Apple" , CategoryId =2, NewPrice = 5000.50m , OldPrice = 6000m},
         new Product { Id = 2, Name = "TV", Description = "TV made by Samsung", CategoryId =1, NewPrice = 1000.50m , OldPrice = 2000m }
        );


    }
}
