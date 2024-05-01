using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebSocket.SignalR.Controllers
{
    [Authorize]
    [ApiVersion("2.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAction(int id)
        {
            return Ok(id);
        }
    }
}
