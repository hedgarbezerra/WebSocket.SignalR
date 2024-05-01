using System.ComponentModel.DataAnnotations;

namespace WebSocket.SignalR.Models.DTOs
{
    public class CreateSeatDTO
    {
        [Required, Key]
        public Guid RoomId { get; set; }
        [Required]
        public int Row { get; set; }
        [Required]
        public int Column { get; set; }
        [Required]
        public bool IsHandicapAccessible { get; set; }
    }

    public class UpdateSeatDTO : CreateSeatDTO
    {
        [Required, Key]
        public Guid Id { get; set; }
    }
}
