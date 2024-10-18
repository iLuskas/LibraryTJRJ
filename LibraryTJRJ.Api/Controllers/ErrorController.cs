using Microsoft.AspNetCore.Mvc;

namespace LibraryTJRJ.Api.Controllers;

public class ErrorController : ControllerBase
{
    [Route("error")]
    public IActionResult Error()
    {
        return Problem();
    }
}
