using Ecom.API.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers;

[ApiController]
[Route("Errors/{statusCode}")]
public class ErrorController : ControllerBase
{
    [HttpGet]
    public IActionResult Error(int statusCode)
    {
        return new ObjectResult(new ResponseAPI(statusCode));
    }

}
