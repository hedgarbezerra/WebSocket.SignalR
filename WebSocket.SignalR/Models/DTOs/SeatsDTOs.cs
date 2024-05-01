namespace WebSocket.SignalR.Models.DTOs
{
    public class CreateSeatDTO
    {
        public Guid RoomId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsHandicapAccessible { get; set; }
    }

    public class UpdateSeatDTO : CreateSeatDTO
    {
        public Guid Id { get; set; }
    }
}
