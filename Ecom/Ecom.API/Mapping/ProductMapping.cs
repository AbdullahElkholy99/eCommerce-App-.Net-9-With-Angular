using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entity.Product;

namespace Ecom.API.Mapping;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        // Add your mappings here
        CreateMap<Product, ProductDTO>()
            .ForMember(x=>x.CategoryName,
            op=>op.MapFrom(src=>src.Category.Name))
            .ReverseMap();

        CreateMap<ProductForCreationDTO, Product>()
            .ForMember(dest=>dest.Photos, opt=>opt.Ignore())
            .ReverseMap();

        CreateMap<ProductForUpdateDTO, Product>()
            .ForMember(dest => dest.Photos, opt => opt.Ignore())
            .ReverseMap();

    }
}
