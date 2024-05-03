using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSocket.SignalR.Interfaces;
using WebSocket.SignalR.Models.DTOs;
using WebSocket.SignalR.Models;
using Microsoft.AspNetCore.Identity;
using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebSocket.SignalR.Controllers
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : BaseAuthenticatedController
    {
        private readonly ILogger<SessionsController> _logger;
        private readonly ISessionsService _sessionsService;
        private readonly IMapper _mapper;

        public SessionsController(ILogger<SessionsController> logger, ISessionsService sessionsService, IMapper mapper, UserManager<AppUser> userManager) : base(userManager)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            ArgumentNullException.ThrowIfNull(sessionsService, nameof(sessionsService));
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

            _logger = logger;
            _sessionsService = sessionsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Result<IEnumerable<Session>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var sessions = _sessionsService.GetSessions();

            return Ok(sessions);
        }

        [HttpGet]
        [Route("{sessionId:guid}")]
        [ProducesResponseType(typeof(Result<Session>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get([FromRoute]Guid sessionId)
        {
            var session = _sessionsService.GetSession(sessionId);
            if (session is null)
                return NotFound();

            return Ok(session);
        }

        [HttpGet]
        [Route("pagination")]
        [ProducesResponseType(typeof(Result<PaginatedList<Session>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPaginated([FromQuery] PaginationInput pagination)
        {
            var sessions = _sessionsService.GetSessions(pagination, HttpContext?.Request?.Path);

            return Ok(sessions);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] CreateSessionDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var session = _mapper.Map<Session>(request);
            var sessionId = _sessionsService.AddSession(session);

            return Ok(sessionId);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] UpdateSessionDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sessionExists = _sessionsService.SessionExists(request.Id);
            if (sessionExists.IsFailed)
                return NotFound(sessionExists);

            var session = _mapper.Map<Session>(request);
            var result = _sessionsService.UpdateSession(session);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{sessionId:guid}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] Guid sessionId)
        {
            var sessionExists = _sessionsService.SessionExists(sessionId);
            if (!sessionExists.IsSuccess)
                return NotFound(sessionExists);

            var result = _sessionsService.DeleteSession(sessionId);

            return Ok(result);
        }


        [HttpPost]
        [Route("{sessionId:guid}/seat")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AssignSeat([FromRoute] Guid sessionId, [FromBody] AssignSeatToUserDTO request)
        {
            var user = GetAuthentcatedUser();
            var result = _sessionsService.AssignSeatToUserSession(request.SeatId, user.Id, sessionId);

            return Ok(result);
        }

        [HttpPost]
        [Route("{sessionId:guid}/seats")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AssignSeats([FromRoute] Guid sessionId, [FromBody] AssignMultipleSeatsToUserDTO request)
        {
            var user = GetAuthentcatedUser();
            var result = _sessionsService.AssignSeatToUserSession(user.Id, sessionId, request.SeatsIds);

            return Ok(result);
        }
    }
}
