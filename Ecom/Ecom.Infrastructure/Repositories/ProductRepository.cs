
using Ecom.Core.Sharing;

namespace Ecom.Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IImageManagementService _imageManagementService;
    public ProductRepository(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
    {
        _context = context;
        _mapper = mapper;
        _imageManagementService = imageManagementService;
    }

    //----------------- Create Operations ----------------
    public async Task<bool> AddAsync(ProductForCreationDTO ProductDto)
    {
        if (ProductDto is null) return false;

        var product = _mapper.Map<Product>(ProductDto);
            
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        var imagePaths =await _imageManagementService.UploadImageAsync(ProductDto.Photos, product.Name);

        var photos = imagePaths.Select(path => new Photo
        {
            Name = path,
            ProductId = product.Id
        }).ToList();

         _context.Photos.AddRange(photos);
        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<IEnumerable<ProductDTO>> GetAllAsync(ProductParams productParams)
    {
        var query = _context.Products
            .Include(c => c.Category)
            .Include(p => p.Photos)
            .AsNoTracking();

        //filtering by categoryId if provided
        if (productParams.CategoryId.HasValue)
            query = query.Where(p => p.CategoryId == productParams.CategoryId.Value);

        //Filtering By Word (Search)
        if(!string.IsNullOrEmpty(productParams.Search))
        {
            var searchWords = productParams.Search.Split(' ');
            query = query
                .Where(m => searchWords.All(word =>
                    m.Name.ToLower().Contains(word.ToLower()) ||
                    m.Description.ToLower().Contains(word.ToLower())
                ));

        }


        if (!string.IsNullOrEmpty(productParams.Sort))
        {
            query = productParams.Sort.ToLower() switch
            {
                "priceace" => query.OrderBy(p => p.NewPrice),
                "pricedce" => query.OrderByDescending(p => p.NewPrice),
                _ => query.OrderBy(n => n.Name)
            };
        }
        else
        {
            // default sort if sort is null/empty
            query = query.OrderBy(n => n.Name);
        }

        //pagination

        productParams.PageNumber = productParams.PageNumber <= 0 ? 1 : productParams.PageNumber;
        productParams.PageSize = productParams.PageSize <= 0 ? 5 : productParams.PageSize;
        query = query
            .Skip((productParams.PageNumber - 1) * productParams.PageNumber)
            .Take(productParams.PageSize);

        var result = _mapper.Map<List<ProductDTO>>(query);

        return result;
    }

    //----------------- Update Operations ----------------
    public async Task<bool> UpdateAsync(ProductForUpdateDTO ProductDto)
    {
        if (ProductDto is null)
            return false;

        var existingProduct = 
            await _context.Products
            .Include(c=>c.Category)
            .Include(p=>p.Photos)
            .FirstOrDefaultAsync(p => p.Id == ProductDto.Id);

        if (existingProduct == null)
            return false;

        _mapper.Map(ProductDto, existingProduct);

        var findPhoto = await _context.Photos
            .Where(p => p.ProductId == existingProduct.Id)
            .ToListAsync();

        foreach (var photo in findPhoto)
            _imageManagementService.DeleteImageAsync(photo.Name);

        _context.Photos.RemoveRange(findPhoto);

        var imagePaths = 
            await _imageManagementService
            .UploadImageAsync(ProductDto.Photos, existingProduct.Name);

        var photos = imagePaths.Select(path => new Photo
        {
            Name = path,
            ProductId = existingProduct.Id
        }).ToList();

        _context.Photos.AddRange(photos);
        await _context.SaveChangesAsync();

        return true;
    }

    //----------------- Delete Operations ----------------
    public async Task DeleteAsync(Product Product)
    {
        var photo = _context.Photos
            .Where(p => p.ProductId == Product.Id)
            .ToList();

        foreach (var p in photo)
            _imageManagementService.DeleteImageAsync(p.Name);

        _context.Products.Remove(Product);
        await _context.SaveChangesAsync();

    }
}
