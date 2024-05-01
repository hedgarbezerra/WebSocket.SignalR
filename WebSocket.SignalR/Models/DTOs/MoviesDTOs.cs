using System.ComponentModel.DataAnnotations;
using static WebSocket.SignalR.Enumerations;

namespace WebSocket.SignalR.Models.DTOs
{
    public class CreateMovieDTO
    {
        [Required, MaxLength(255), ]
        public string Name { get; set; }
        [Required]
        public string Sinopsys { get; set; }
        [Required]
        public string Classification { get; set; }
        [Required]
        public string DirectorName { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public DateTime Release { get; set; }
        public List<string> Starring { get; set; } = new List<string>();
    }
    public class UpdateMovieDTO
    {
        [Required, Key]
        public Guid Id { get; set; }
    }
}
