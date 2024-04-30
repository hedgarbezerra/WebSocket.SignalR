namespace WebSocket.SignalR.Data
{
    public class Session
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public Guid RoomId { get; set; }
        public string Language { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public List<SeatTaken> SeatsTaken { get; set; } = new List<SeatTaken>();
        public bool IsFull { get => Room?.Seats.Count == SeatsTaken.Count; }
        public virtual Movie? Movie { get; set; }
        public virtual Room? Room { get; set; }
    }
}
