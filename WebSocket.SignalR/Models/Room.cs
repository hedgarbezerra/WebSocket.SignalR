using static WebSocket.SignalR.Enumerations;

namespace WebSocket.SignalR.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ERoomType Type { get; set; }
        public int SeatCount { get => Seats.Count; }
        public bool IsEmpty { get => !Seats.Any(); }

        public virtual List<Seat> Seats { get; set; } = new List<Seat>();

        public Seat? GetSeat(int row, int column) => Seats.FirstOrDefault(s => s.Row == row && s.Column == column);
        public static Room Create(string name, ERoomType type) => new Room { Id = Guid.NewGuid(), Name = name, Type = type };
    }
}
