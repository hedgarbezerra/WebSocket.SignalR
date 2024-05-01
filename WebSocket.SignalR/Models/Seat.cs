namespace WebSocket.SignalR.Models
{
    public class Seat
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsHandicapAccessible { get; set; }
        public virtual Room? Room { get; set; }

        public static Seat Create(Guid roomId, int row, int column, bool isHandicapAccessible = false)
        {
            var seat = new Seat
            {
                Id = Guid.NewGuid(),
                RoomId = roomId,
                Row = row,
                Column = column,
                IsHandicapAccessible = isHandicapAccessible
            };
            return seat;
        }
    }

    public class SeatTaken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SessionId { get; set; }
        public Guid SeatId { get; set; }
        public virtual AppUser? User { get; set; }
        public virtual Session? Session { get; set; }
        public virtual Seat? Seat { get; set; }

        public static SeatTaken Create(Guid userId, Guid sessionId, Guid seatId) =>
            new SeatTaken { Id = Guid.NewGuid(), UserId = userId, SessionId = sessionId, SeatId = seatId };
    }
}
