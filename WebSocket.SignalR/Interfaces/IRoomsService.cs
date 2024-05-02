using System.Linq.Expressions;
using WebSocket.SignalR.Models;

namespace WebSocket.SignalR.Interfaces
{
    public interface IRoomsService
    {
        Room? GetRoom(Guid roomId);
        IReadOnlyList<Room> GetRooms();
        IReadOnlyList<Room> GetRooms(Expression<Func<Room, bool>> filter);
        PaginatedList<Room> GetRooms(PaginationInput pagination, string route);
        Guid AddRoom(Room room);
        bool UpdateRoom(Room room);
        bool DeleteRoom(Guid roomId);
        bool RoomExists(Guid roomId);

        Seat? GetSeat(Guid id);
        Seat? GetSeat(Guid roomId, int row, int column);
        bool UpdateSeat(Seat seat);
        bool DeleteSeat(Guid seatId);
        IReadOnlyList<Seat> GetSeats(Guid roomId);
        bool AddSeatToRoom(Seat seat, Guid roomId);
        bool AddSeatToRoom(Guid seatId, Guid roomId);
        bool AddSeatToRoom(Seat seat, Room room);

    }
}
