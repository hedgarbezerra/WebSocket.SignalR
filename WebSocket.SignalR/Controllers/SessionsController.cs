using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSocket.SignalR.Interfaces;
using WebSocket.SignalR.Models.DTOs;
using WebSocket.SignalR.Models;
using Microsoft.AspNetCore.Identity;
using FluentResults;
using WebSocket.SignalR.Extensions;

namespace WebSocket.SignalR.Controllers
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : BaseAuthenticatedController
    {
        private readonly ISessionsService _sessionsService;
        private readonly IMapper _mapper;

        public SessionsController(ISessionsService sessionsService, IMapper mapper, UserManager<AppUser> userManager) : base(userManager)
        {
            ArgumentNullException.ThrowIfNull(sessionsService, nameof(sessionsService));
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

            _sessionsService = sessionsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResultDTO<IEnumerable<Session>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            throw new Exception();
            var sessions = _sessionsService.GetSessions().FromResult();

            return Ok(sessions);
        }

        [HttpGet]
        [Route("{sessionId:guid}")]
        [ProducesResponseType(typeof(ResultDTO<Session>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Get([FromRoute]Guid sessionId)
        {
            var session = _sessionsService.GetSession(sessionId).FromResult();
            if (!session.Success)
                return NotFound(session);

            return Ok(session);
        }

        [HttpGet]
        [Route("pagination")]
        [ProducesResponseType(typeof(ResultDTO<PaginatedList<Session>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult GetPaginated([FromQuery] PaginationInput pagination)
        {
            var sessions = _sessionsService.GetSessions(pagination, HttpContext?.Request?.Path).FromResult();

            return Ok(sessions);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResultDTO<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] CreateSessionDTO request)
        {
            var session = _mapper.Map<Session>(request);
            var sessionId = _sessionsService.AddSession(session).FromResult();

            return Created(HttpContext.Request.Path, sessionId);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] UpdateSessionDTO request)
        {
            var sessionExists = _sessionsService.SessionExists(request.Id).FromResult();
            if (!sessionExists.Success)
                return NotFound(sessionExists);

            var session = _mapper.Map<Session>(request);
            var result = _sessionsService.UpdateSession(session);

            return Ok(result.FromResult());
        }

        [HttpDelete]
        [Route("{sessionId:guid}")]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] Guid sessionId)
        {
            var sessionExists = _sessionsService.SessionExists(sessionId).FromResult();
            if (!sessionExists.Success)
                return NotFound(sessionExists);

            var result = _sessionsService.DeleteSession(sessionId).FromResult();

            return Ok(result);
        }


        [HttpGet]
        [Route("{sessionId:guid}/seats/available")]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult GetEmptySeats([FromRoute] Guid sessionId)
        {
            var result = _sessionsService.GetSeatsEmpty(sessionId).FromResult();

            return Ok(result);
        }

        [HttpGet]
        [Route("{sessionId:guid}/seats/taken")]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult GetSeatsTaken([FromRoute] Guid sessionId)
        {
            var result = _sessionsService.GetSeatsTaken(sessionId).FromResult();

            return Ok(result);
        }

        [HttpPost]
        [Route("{sessionId:guid}/seats/acquire/{seatId:guid}")]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult AssignSeat([FromRoute] Guid sessionId, [FromRoute] Guid seatId)
        {
            var user = GetAuthentcatedUser();
            var result = _sessionsService.AssignSeatToUserSession(seatId, sessionId, user).FromResult();

            return Ok(result);
        }

        [HttpPost]
        [Route("{sessionId:guid}/seats/acquire")]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult AssignSeats([FromRoute] Guid sessionId, [FromBody] AssignMultipleSeatsToUserDTO request)
        {
            var user = GetAuthentcatedUser();
            var result = _sessionsService.AssignSeatToUserSession(sessionId, request.SeatsIds, user).FromResult();

            return Ok(result);
        }
    }
}
