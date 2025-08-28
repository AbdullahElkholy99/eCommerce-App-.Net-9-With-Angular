using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entity.Product;

namespace Ecom.API.Mapping;

public class PhotoMapping : Profile
{
    public PhotoMapping()
    {
        // Add your mappings here
        CreateMap<Photo, PhotoDTO>()
            .ReverseMap();

    }
}
