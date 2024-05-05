using FluentResults;
using System.Linq.Expressions;
using WebSocket.SignalR.Models;

namespace WebSocket.SignalR.Interfaces
{
    public interface IRoomsService
    {
        Result<Room?> GetRoom(Guid roomId);
        Result<List<Room>> GetRooms();
        Result<List<Room>> GetRooms(Expression<Func<Room, bool>> filter);
        Result<PaginatedList<Room>> GetRooms(PaginationInput pagination, string route);
        Result<Guid> AddRoom(Room room);
        Result UpdateRoom(Room room);
        Result DeleteRoom(Guid roomId);
        Result RoomExists(Guid roomId);

        Result AddSeat(Seat seat, Guid roomId);
        Result AddSeat(Seat seat, Room room);
        Result<Seat?> GetSeat(Guid id);
        Result<Seat?> GetSeat(Guid roomId, int row, int column);
        Result DeleteSeat(Guid seatId);
        Result<Seat?> DeleteSeat(Guid roomId, int row, int column);
        Result<List<Seat>> GetSeats(Guid roomId);

    }
}
