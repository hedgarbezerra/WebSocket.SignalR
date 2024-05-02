using System.Linq.Expressions;
using WebSocket.SignalR.Configuration;
using WebSocket.SignalR.Interfaces;
using WebSocket.SignalR.Models;

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

        public Guid AddRoom(Room room)
        {
            ArgumentNullException.ThrowIfNull(room);

            var insertedRoom = _roomsRepository.Add(room);
            _roomsRepository.SaveChanges();

            return insertedRoom.Id;
        }

        public bool UpdateRoom(Room room)
        {
            ArgumentNullException.ThrowIfNull(room);

            var updatedRoom = _roomsRepository.Update(room);

            return _roomsRepository.SaveChanges();
        }

        public bool DeleteRoom(Guid roomId)
        {
            if (roomId == Guid.Empty)
                throw new ArgumentNullException(nameof(roomId));

            _roomsRepository.Delete(roomId);
            return _roomsRepository.SaveChanges();
        }

        public Room? GetRoom(Guid id)
        {
            var room = _roomsRepository.Get(id);

            return room;
        }

        public IReadOnlyList<Room> GetRooms()
        {
            var rooms = _roomsRepository.Get().ToList().AsReadOnly();
            return rooms;
        }

        public IReadOnlyList<Room> GetRooms(Expression<Func<Room, bool>> filter)
        {
            var rooms = _roomsRepository.Get(filter).ToList().AsReadOnly();
            return rooms;
        }

        public PaginatedList<Room> GetRooms(PaginationInput pagination, string route)
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
            return paginated;
        }

        public Seat? GetSeat(Guid id)
        {
            var seat = _seatsRepository.Get(id);

            return seat;
        }

        public Seat? GetSeat(Guid roomId, int row, int column)
        {
            var room = _roomsRepository.Get(roomId);
            if (room is null)
                return null;

            var seat = room.GetSeat(row, column);

            return seat;
        }

        public IReadOnlyList<Seat> GetSeats(Guid roomId)
        {
            var room = _roomsRepository.Get(roomId);
            if (room is null)
                return [];

            return room.Seats;
        }

        public bool DeleteSeat(Guid seatId)
        {
            _seatsRepository.Delete(seatId);

            return _roomsRepository.SaveChanges();
        }

        public bool UpdateSeat(Seat seat)
        {
            ArgumentNullException.ThrowIfNull(seat);

            var updatedSeat = _seatsRepository.Update(seat);

            return _seatsRepository.SaveChanges();
        }
        public bool AddSeatToRoom(Seat seat, Guid roomId)
        {
            ArgumentNullException.ThrowIfNull(seat);
            var room = _roomsRepository.Get(roomId);
            if (room is null)
                return false;

            room.Seats.Add(seat);

            return _seatsRepository.SaveChanges();
        }
        public bool AddSeatToRoom(Seat seat, Room room)
        {
            ArgumentNullException.ThrowIfNull(seat);
            ArgumentNullException.ThrowIfNull(room);

            room.Seats.Add(seat);

            return _roomsRepository.SaveChanges();
        }

        public bool AddSeatToRoom(Guid seatId, Guid roomId)
        {
            var seat = _seatsRepository.Get(seatId);
            var room = _roomsRepository.Get(roomId);
            if (room is null || seat is null)
                return false;

            room.Seats.Add(seat);

            return _seatsRepository.SaveChanges();
        }

        public bool RoomExists(Guid roomId) => _roomsRepository.Get(m => m.Id == roomId).Any();
    }
}
