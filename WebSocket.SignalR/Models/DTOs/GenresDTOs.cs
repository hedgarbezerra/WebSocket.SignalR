using System.ComponentModel.DataAnnotations;

namespace WebSocket.SignalR.Models.DTOs
{
    public record CreateGenreDTO
    {
        [Required, MaxLength(255)]
        public string Name { get; set; } = string.Empty;
    }

    public record UpdateGenreDTO : CreateGenreDTO
    {
        [Required, Key]
        public Guid Id { get; set; }
    }
}
