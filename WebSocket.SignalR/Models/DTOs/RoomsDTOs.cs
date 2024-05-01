using System.ComponentModel.DataAnnotations;
using static WebSocket.SignalR.Enumerations;

namespace WebSocket.SignalR.Models.DTOs
{
    public class CreateRoomDTO
    {
        [Required, MaxLength(255)]
        public string Name { get; set; }
        [Required]
        public ERoomType Type { get; set; }
    }
    public class UpdateRoomDTO : CreateRoomDTO
    {
        [Required]
        public Guid Id { get; set; }
    }
}
