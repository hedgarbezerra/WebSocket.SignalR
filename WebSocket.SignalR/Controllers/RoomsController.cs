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
    public class RoomsController : BaseAuthenticatedController
    {
        private readonly ILogger<RoomsController> _logger;
        private readonly IRoomsService _roomsService;
        private readonly IMapper _mapper;

        public RoomsController(ILogger<RoomsController> logger, IRoomsService roomsService, IMapper mapper, UserManager<AppUser> userManager) : base(userManager)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            ArgumentNullException.ThrowIfNull(roomsService, nameof(roomsService));
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

            _logger = logger;
            _roomsService = roomsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Result<IEnumerable<Room>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var rooms = _roomsService.GetRooms();

            return Ok(rooms);
        }

        [HttpGet]
        [Route("{roomId:guid}")]
        [ProducesResponseType(typeof(Result<Room>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get([FromRoute] Guid roomId)
        {
            var room = _roomsService.GetRoom(roomId);
            if (room is null)
                return NotFound();

            return Ok(room);
        }

        [HttpGet]
        [Route("pagination")]
        [ProducesResponseType(typeof(Result<PaginatedList<Room>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPaginated([FromQuery] PaginationInput pagination)
        {
            var rooms = _roomsService.GetRooms(pagination, HttpContext?.Request?.Path);

            return Ok(rooms);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] CreateRoomDTO request)
        {
            var room = _mapper.Map<Room>(request);
            var roomId = _roomsService.AddRoom(room);

            return Ok(roomId);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] UpdateRoomDTO request)
        {
            var roomExists = _roomsService.RoomExists(request.Id);
            if (roomExists.IsFailed)
                return NotFound(roomExists);

            var room = _mapper.Map<Room>(request);
            var rooms = _roomsService.UpdateRoom(room);

            return Ok(rooms);
        }

        [HttpDelete]
        [Route("{roomId:guid}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] Guid roomId)
        {
            var roomExists = _roomsService.RoomExists(roomId);
            if (roomExists.IsFailed)
                return NotFound(roomExists);

            var result = _roomsService.DeleteRoom(roomId);

            return Ok(result);
        }
    }
}
