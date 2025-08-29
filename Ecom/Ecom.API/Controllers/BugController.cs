using AutoMapper;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BugController : BaseController
{
    public BugController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    [HttpGet("not-found")]
    public async Task<IActionResult> GetNotFound()
    {
        var category = _unitOfWork.CategoryRepository.GetByIdAsync(42);

        if (category == null) return NotFound(new { message = "Not Found" });

        return Ok();
    }
    [HttpGet("server-error")]
    public async Task<IActionResult> GetServerError()
    {
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(42);
        
        category.Name = "";//Throw exception    

        return Ok(category);
    }
    [HttpGet("bad-request/{id}")]
    public IActionResult GetBadRequest(int id)
    {
        return Ok();
    }
    [HttpGet("bad-request")]
    public IActionResult GetBadRequest()
    {
        return BadRequest();
    }


}
