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
    public class RoomsController : BaseAuthenticatedController
    {
        private readonly IRoomsService _roomsService;
        private readonly IMapper _mapper;

        public RoomsController(IRoomsService roomsService, IMapper mapper, UserManager<AppUser> userManager) : base(userManager)
        {
            ArgumentNullException.ThrowIfNull(roomsService, nameof(roomsService));
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

            _roomsService = roomsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResultDTO<IEnumerable<Room>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var rooms = _roomsService.GetRooms().FromResult();

            return Ok(rooms);
        }

        [HttpGet]
        [Route("{roomId:guid}")]
        [ProducesResponseType(typeof(ResultDTO<Room>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Get([FromRoute] Guid roomId)
        {
            var room = _roomsService.GetRoom(roomId).FromResult();
            if (!room.Success)
                return NotFound(room);

            return Ok(room);
        }

        [HttpGet]
        [Route("pagination")]
        [ProducesResponseType(typeof(ResultDTO<PaginatedList<Room>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult GetPaginated([FromQuery] PaginationInput pagination)
        {
            var rooms = _roomsService.GetRooms(pagination, HttpContext?.Request?.Path).FromResult();

            return Ok(rooms);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResultDTO<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] CreateRoomDTO request)
        {
            var room = _mapper.Map<Room>(request);
            var roomId = _roomsService.AddRoom(room).FromResult();

            return Created(HttpContext.Request.Path, roomId);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] UpdateRoomDTO request)
        {
            var roomExists = _roomsService.RoomExists(request.Id).FromResult();
            if (!roomExists.Success)
                return NotFound(roomExists);

            var room = _mapper.Map<Room>(request);
            var rooms = _roomsService.UpdateRoom(room).FromResult();

            return Ok(rooms);
        }

        [HttpDelete]
        [Route("{roomId:guid}")]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] Guid roomId)
        {
            var roomExists = _roomsService.RoomExists(roomId).FromResult();
            if (!roomExists.Success)
                return NotFound(roomExists);

            var result = _roomsService.DeleteRoom(roomId).FromResult();

            return Ok(result);
        }

        [HttpGet]
        [Route("{roomId:guid}/seats")]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult GetSeats([FromRoute] Guid roomId)
        {
            var roomExists = _roomsService.RoomExists(roomId).FromResult();
            if (!roomExists.Success)
                return NotFound(roomExists);

            var result = _roomsService.GetSeats(roomId).FromResult();

            return Ok(result);
        }

        [HttpPost]
        [Route("{roomId:guid}/seats")]
        [ProducesResponseType(typeof(ResultDTO<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult AddSeat([FromRoute] Guid roomId, [FromBody] CreateSeatDTO seatDto)
        {
            var roomExists = _roomsService.RoomExists(roomId).FromResult();
            if (!roomExists.Success)
                return NotFound(roomExists);

            var seat = _mapper.Map<Seat>(seatDto);
            var result = _roomsService.AddSeat(seat, roomId).FromResult();

            return Ok(result);
        }

        [HttpDelete]
        [Route("{roomId:guid}/seats")]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveSeat([FromRoute] Guid roomId, [FromQuery] int row, [FromQuery] int column)
        {
            var roomExists = _roomsService.RoomExists(roomId).FromResult();
            if (!roomExists.Success)
                return NotFound(roomExists);

            var result = _roomsService.DeleteSeat(roomId, row, column).FromResult();

            return Ok(result);
        }


        [HttpDelete]
        [Route("{roomId:guid}/seats/{seatId:guid}")]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveSeat([FromRoute] Guid roomId, [FromRoute] Guid seatId)
        {
            var roomExists = _roomsService.RoomExists(roomId).FromResult();
            if (!roomExists.Success)
                return NotFound(roomExists);

            var result = _roomsService.DeleteSeat(seatId).FromResult();

            return Ok(result);
        }
    }
}
