using AutoMapper;
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
    public async Task<IActionResult> AddCategory([FromBody] CategoryForCreationDTO categoryDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid category data.");

            if (categoryDto == null)
                return BadRequest("Category data is null.");

            var category = _mapper.Map<Category>(categoryDto);

            await _unitOfWork.CategoryRepository.AddAsync(category);

            return Ok(new {Message="Category added successfully", CategoryId = category.Id});
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    //----------------- Read Operations ----------------
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllCategories()
    {
        try
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
           
            if (categories == null || !categories.Any())
            {
                return NotFound("No categories found.");
            }

            return Ok(categories);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
       
    }
    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (category == null  )
            {
                return NotFound("No category found.");
            }

            return Ok(category);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    //----------------- Update Operations ----------------
    [HttpPut("update-category")]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryForUpdateDTO categoryDto)
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

            return Ok("Category updated successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    //----------------- Delete Operations ----------------
    [HttpDelete("delete-category/{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            await _unitOfWork.CategoryRepository.DeleteAsync(id);

            return Ok("Category deleted successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }






}

