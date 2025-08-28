using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entity.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    public ProductsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    //----------------- Create Operations ----------------
    [HttpPost("add-product")]
    public async Task<IActionResult> Add([FromForm] ProductForCreationDTO productDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid product data.");

            if (productDto == null)
                return BadRequest("product data is null.");

            await _unitOfWork.ProductRepository.AddAsync(productDto);

            return Ok(new ResponseAPI(200, $"Product added successfully Product "));
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseAPI(400,ex.Message));
        }
    }

    //----------------- Read Operations ----------------
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var products = 
                await _unitOfWork
                .ProductRepository
                .GetAllAsync(x=>x.Category, x=>x.Photos);
           
            if (products is null || !products.Any())
                return BadRequest( new ResponseAPI(400));

            var result = _mapper.Map<IEnumerable<ProductDTO>>(products);
           
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
       
    }
    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var product = await _unitOfWork
                .ProductRepository
                .GetByIdAsync(id,x => x.Category, x => x.Photos);

            if (product is null  )
                return BadRequest(new ResponseAPI(400,$"Not Found product Id = {id}"));

            var result = _mapper.Map<ProductDTO>(product);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //----------------- Update Operations ----------------
    [HttpPut("update-product")]
    public async Task<IActionResult> Update([FromForm] ProductForUpdateDTO productDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid product data.");

            await _unitOfWork.ProductRepository.UpdateAsync(productDto);

            return Ok(new ResponseAPI(200, $"Product updated successfully"));

        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseAPI(400, ex.Message));
        }
    }

    //----------------- Delete Operations ----------------
    [HttpDelete("delete-category/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id,x=>x.Category,x=>x.Photos);

            await _unitOfWork.ProductRepository.DeleteAsync(product);

            return Ok(new ResponseAPI( 200,"Category deleted successfully."));
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseAPI(400, ex.Message));
        }
    }


}

