using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebSocket.SignalR.Controllers
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MovieSessionsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAction(int id)
        {
            return Ok(id);
        }
    }
}
