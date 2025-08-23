using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entity.Product;

namespace Ecom.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Add your mappings here
        CreateMap<CategoryForCreationDTO, Category>();
        CreateMap<CategoryForUpdateDTO, Category>().ReverseMap();

    }
}
