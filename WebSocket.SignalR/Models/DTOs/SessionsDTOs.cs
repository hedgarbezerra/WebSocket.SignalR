using System.ComponentModel.DataAnnotations;

namespace WebSocket.SignalR.Models.DTOs
{
    public class CreateSessionDTO
    {
        [Required]
        public Guid MovieId { get; set; }
        [Required]
        public Guid RoomId { get; set; }
        [Required, MaxLength(255)]
        public string Language { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
    public class UpdateSessionDTO : CreateSessionDTO
    {
        [Required, Key]
        public Guid Id { get; set; }
    }

}
