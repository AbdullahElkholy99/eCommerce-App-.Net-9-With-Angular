using Ecom.Core.Entity.Product;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;

namespace Ecom.Infrastructure.Repositories;

public class CategoryRepository : GenericRepository<Category> , ICategoryRepository
{
    public CategoryRepository(AppDbContext context):base(context)
    {
    }
}
