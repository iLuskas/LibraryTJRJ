using Microsoft.AspNetCore.Mvc;

namespace LibraryTJRJ.Api.Controllers;

[Route("error")]
public class ErrorController : ControllerBase
{    
    [HttpGet]
    public IActionResult Error()
    {
        return Problem();
    }
}
