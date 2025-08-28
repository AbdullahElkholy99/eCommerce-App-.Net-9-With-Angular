using Ecom.Core.Entity.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Data.Configuration;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.HasData(
            new Photo { Id = 1, Name = "test", ProductId = 1 },
            new Photo { Id = 2, Name ="test2", ProductId = 1 }
        );


    }
}

 