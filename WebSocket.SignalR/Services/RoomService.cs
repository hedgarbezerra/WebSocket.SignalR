using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq.Expressions;
using WebSocket.SignalR.Configuration;
using WebSocket.SignalR.Interfaces;
using WebSocket.SignalR.Models;
using static WebSocket.SignalR.Data.Repository.Repositories;

namespace WebSocket.SignalR.Services
{
    [BindInterface(typeof(IRoomsService))]
    public class RoomService : IRoomsService
    {
        private readonly IRepository<Room> _roomsRepository;
        private readonly IRepository<Seat> _seatsRepository;
        private readonly IUriService _uriService;

        public RoomService(IRepository<Room> roomsRepository, IRepository<Seat> seatsRepository, IUriService uriService)
        {
            ArgumentNullException.ThrowIfNull(roomsRepository, nameof(roomsRepository));
            ArgumentNullException.ThrowIfNull(seatsRepository, nameof(seatsRepository));
            ArgumentNullException.ThrowIfNull(uriService, nameof(uriService));

            _roomsRepository = roomsRepository;
            _seatsRepository = seatsRepository;
            _uriService = uriService;
        }

        public Result<Guid> AddRoom(Room room)
        {
            if (room is null)
                return Result.Fail($"The parameter '{nameof(room)}' provided cannot be null.");

            var insertedRoom = _roomsRepository.Add(room);

            return Result.Ok(_roomsRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok(insertedRoom.Id).WithSuccess($"Room '{room.Name}' created with identifier '{room.Id}'.")
                    : Result.Fail($"Room '{room.Name}' was not created."));
        }
        public Result UpdateRoom(Room room)
        {
            if (room is null)
                return Result.Fail($"The parameter '{nameof(room)}' provided cannot be null.");

            var updatedRoom = _roomsRepository.Update(room);

            return Result.Ok(_roomsRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"Room with identifier '{room.Id}' updated successfully.")
                    : Result.Fail($"Room with identifier '{room.Id}' was not updated."));
        }
        public Result DeleteRoom(Guid roomId)
        {
            var result = RoomExists(roomId);
            if (result.IsFailed)
                return result;

            _roomsRepository.Delete(roomId);

            return Result.Ok(_roomsRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"Room with identifier '{roomId}' successfully deleted.")
                    : Result.Fail($"Room with identifier '{roomId}' was not deleted."));
        }
        public Result RoomExists(Guid roomId)
        {
            bool roomFound = _roomsRepository.Get(m => m.Id == roomId).Any();

            return Result.OkIf(roomFound, $"Room with identifier '{roomId}' not found.");
        }
        public Result<Room?> GetRoom(Guid roomId)
        {
            var room = _roomsRepository.Get(roomId);

            return Result.Ok(room is not null)
                .Bind(v => v ?
                    Result.Ok(room)
                    : Result.Fail($"Room with identifier '{roomId}' not found."));
        }
        public Result<List<Room>> GetRooms()
        {
            var rooms = _roomsRepository.Get().ToList();

            return Result.Ok(rooms).WithSuccess($"Total count of rooms: {rooms.Count}.");
        }
        public Result<List<Room>> GetRooms(Expression<Func<Room, bool>> filter)
        {
            var rooms = _roomsRepository.Get(filter).ToList();

            return Result.Ok(rooms).WithSuccess($"Total count of rooms: {rooms.Count}.");
        }
        public Result<PaginatedList<Room>> GetRooms(PaginationInput pagination, string route)
        {
            var rooms = _roomsRepository.Get();
            if (!string.IsNullOrEmpty(pagination.SearchTerm))
            {
                var actualTerm = pagination.SearchTerm.Split(' ');
                for (int i = 0; i < actualTerm.Length; i++)
                {
                    var term = actualTerm[i];
                    rooms = rooms.Where(t => t.Name.Contains(term));
                }
            }

            var paginated = new PaginatedList<Room>(rooms, _uriService, route, pagination.Index, pagination.Size);
            return Result.Ok(paginated).WithSuccess($"page '{paginated.PageIndex}' from '{paginated.TotalPages}' - Total itens: {paginated.TotalCount}");
        }

        public Result<Seat?> GetSeat(Guid id)
        {
            var seat = _seatsRepository.Get(id);

            return Result.Ok(seat is not null)
                .Bind(v => v ?
                    Result.Ok(seat)
                    : Result.Fail($"Seat with identifier '{id}' not found."));
        }
        public Result<Seat?> GetSeat(Guid roomId, int row, int column)
        {
            var roomResult = GetRoom(roomId);
            if (roomResult.IsFailed)
                return roomResult.ToResult();

            var seat = roomResult.Value.GetSeat(row, column);

            return Result.Ok(seat is not null)
                .Bind(v => v ?
                    Result.Ok(seat)
                    : Result.Fail($"Seat with identifier at position '{row}'x'{column}' not found."));
        }
        public Result DeleteSeat(Guid seatId)
        {
            var seat = GetSeat(seatId);
            if (seat.IsFailed)
                return seat.ToResult();

            _seatsRepository.Delete(seatId);

            return Result.Ok(_seatsRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"Seat with identifier '{seatId}' successfully deleted.")
                    : Result.Fail($"Seat with identifier '{seatId}' was not deleted."));
        }

        public Result<Seat?> DeleteSeat(Guid roomId, int row, int column)
        {
            var room = GetRoom(roomId);
            if (room.IsFailed)
                return room.ToResult();

            var seat = room.Value.GetSeat(row, column);
            if (seat is null)
                return Result.Fail($"Seat with identifier at position '{row}'x'{column}' not found.");

            room.Value.Seats.Remove(seat);

            return Result.Ok(_seatsRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"Seat with identifier '{seat.Id}' successfully deleted.")
                    : Result.Fail($"Seat with identifier '{seat.Id}' was not deleted."));
        }

        public Result<List<Seat>> GetSeats(Guid roomId)
        {
            var room = GetRoom(roomId);
            if (room.IsFailed)
                return room.ToResult();

            return Result.Ok(room.Value.Seats);
        }

        //TODO: utilizar o seat repo e adicionar o assento ao repositório antes de adicionar à sala
        public Result AddSeat(Seat seat, Guid roomId)
        {
            var room = GetRoom(roomId);
            if (room.IsFailed)
                return room.ToResult();

            var seatInserted = _seatsRepository.Add(seat);
            room.Value.Seats.Add(seatInserted);

            return Result.Ok(_roomsRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"Seat was assiged to the room '{room.Value.Name}' successfully.")
                    : Result.Fail($"Seat was not assigned to the room."));
        }

        public Result AddSeat(Seat seat, Room room)
        {
            room.Seats.Add(seat);

            return Result.Ok(_roomsRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"Seat was assiged to the room '{room.Name}' successfully.")
                    : Result.Fail($"Seat was not assigned to the room."));
        }
    }
}
