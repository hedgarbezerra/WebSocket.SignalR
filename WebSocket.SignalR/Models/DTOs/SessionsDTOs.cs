using System.ComponentModel.DataAnnotations;

namespace WebSocket.SignalR.Models.DTOs
{
    public class CreateSessionDTO
    {
        [Required]
        public Guid MovieId { get; set; }
        [Required]
        public Guid RoomId { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
    public class UpdateSessionDTO : CreateSessionDTO
    {
        [Required]
        public Guid Id { get; set; }
    }

}
