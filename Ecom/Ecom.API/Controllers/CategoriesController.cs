using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entity.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : BaseController
{
    public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }


    //----------------- Create Operations ----------------
    [HttpPost("add-category")]
    public async Task<IActionResult> Add([FromBody] CategoryForCreationDTO categoryDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid category data.");

            if (categoryDto == null)
                return BadRequest("Category data is null.");

            var category = _mapper.Map<Category>(categoryDto);

            await _unitOfWork.CategoryRepository.AddAsync(category);

            return Ok(new ResponseAPI( 200,$"Category added successfully CategoryId ={ category.Id}" ));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //----------------- Read Operations ----------------
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var categories = 
                await _unitOfWork.CategoryRepository.GetAllAsync();
           
            if (categories is null || !categories.Any())
                return BadRequest( new ResponseAPI(400));

            return Ok(categories);
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
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (category is null  )
                return BadRequest(new ResponseAPI(400,$"Not Found Category Id = {id}"));
                
            return Ok(category);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //----------------- Update Operations ----------------
    [HttpPut("update-category")]
    public async Task<IActionResult> Update([FromBody] CategoryForUpdateDTO categoryDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid category data.");

            var existingCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryDto.Id);

            if (existingCategory == null)
                return NotFound("Category not found.");

            _mapper.Map(categoryDto, existingCategory);

            await _unitOfWork.CategoryRepository.UpdateAsync(existingCategory);

            return Ok(new ResponseAPI(200, $"Category updated successfully"));

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
            await _unitOfWork.CategoryRepository.DeleteAsync(id);

            return Ok(new ResponseAPI( 200,"Category deleted successfully."));
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseAPI(400, ex.Message));
        }
    }


}

