using System.ComponentModel.DataAnnotations;

namespace WebSocket.SignalR.Models.DTOs
{
    public class CreateGenreDTO
    {
        [Required, MaxLength(255)]
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateGenreDTO
    {
        [Required, Key]
        public Guid Id { get; set; }
    }
}
