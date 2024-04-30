using static WebSocket.SignalR.Enumerations;

namespace WebSocket.SignalR.Data
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ERoomType Type { get; set; }
        public int SeatCount { get => Seats.Count; }
        public bool IsEmpty { get => Seats.Any(); }

        public virtual List<Seat> Seats { get; set; } = new List<Seat>();
        public virtual List<Session> Sessions { get; set; } = new List<Session>();
        public Seat? GetSeat(int row, int column) => Seats.FirstOrDefault(s => s.Row == row && s.Column == column);
    }

}
