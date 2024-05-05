using System.ComponentModel.DataAnnotations;

namespace WebSocket.SignalR.Models.DTOs
{
    public record CreateSeatDTO
    {
        [Required, Key]
        public Guid RoomId { get; set; }
        [Required]
        public int? Row { get; set; }
        [Required]
        public int? Column { get; set; }
        [Required]
        public bool IsHandicapAccessible { get; set; }
    }

    public record UpdateSeatDTO : CreateSeatDTO
    {
        [Required, Key]
        public Guid Id { get; set; }
    }
}
