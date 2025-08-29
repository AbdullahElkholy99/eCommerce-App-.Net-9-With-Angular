using Ecom.Core.DTO;
using Ecom.Core.Entity.Product;
using Ecom.Core.Sharing;

namespace Ecom.Core.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    //for future Product-specific methods

    // ---------------- Create Operations : ------------
    Task<bool> AddAsync(ProductForCreationDTO ProductDto);

    // ----------------- Read Operations : ------------
    Task<IEnumerable<ProductDTO>> GetAllAsync(ProductParams productParams);

    // ---------------- Update Operations : ------------
    Task<bool> UpdateAsync(ProductForUpdateDTO ProductDto);

    // ---------------- Delete Operations : ------------
    Task DeleteAsync(Product Product);
}
