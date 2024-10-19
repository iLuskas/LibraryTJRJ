using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryTJRJ.Api.Controllers
{
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/user")]
    public class UserController : ApiController
    {
        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok();
        }
    }
}
