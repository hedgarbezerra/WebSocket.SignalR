namespace WebSocket.SignalR.Data
{
    public class Seat
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsHandicapAccessible { get; set; }
        public virtual Room? Room { get; set; } 
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
    }
}
