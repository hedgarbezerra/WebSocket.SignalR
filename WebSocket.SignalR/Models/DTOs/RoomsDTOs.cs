using System.ComponentModel.DataAnnotations;
using static WebSocket.SignalR.Enumerations;

namespace WebSocket.SignalR.Models.DTOs
{
    public record CreateRoomDTO
    {
        [Required, MaxLength(255)]
        public string Name { get; set; }
        [Required]
        public ERoomType Type { get; set; }
    }
    public record UpdateRoomDTO : CreateRoomDTO
    {
        [Required, Key]
        public Guid Id { get; set; }
    }
}
