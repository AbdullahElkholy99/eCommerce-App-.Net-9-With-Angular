using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entity.Product;

namespace Ecom.API.Mapping;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        // Add your mappings here
        CreateMap<CategoryForCreationDTO, Category>();
        CreateMap<CategoryForUpdateDTO, Category>().ReverseMap();

    }
}
