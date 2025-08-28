using Ecom.Core.DTO;
using Ecom.Core.Entity.Product;

namespace Ecom.Core.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    //for future Product-specific methods
    Task<bool> AddAsync(ProductForCreationDTO ProductDto);

    Task<bool> UpdateAsync(ProductForUpdateDTO ProductDto);
    Task DeleteAsync(Product Product);
}
