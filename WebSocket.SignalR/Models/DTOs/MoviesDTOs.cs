using System.ComponentModel.DataAnnotations;

namespace WebSocket.SignalR.Models.DTOs
{
    public record CreateMovieDTO
    {
        [Required, MaxLength(255), ]
        public string Name { get; set; }
        [Required]
        public string Sinopsys { get; set; }
        [Required]
        public int Classification { get; set; }
        [Required]
        public string DirectorName { get; set; }
        [Required]
        public double DurationInMinutes { get; set; }

        [Required]
        public DateTime Release { get; set; }
        public List<string> Starring { get; set; } = new List<string>();
        public List<Guid> Genres { get; set; } = new List<Guid>();
    }

    public record UpdateMovieDTO: CreateMovieDTO
    {
        [Required, Key]
        public Guid Id { get; set; }
    }

    public record AssignGenresToMovieDTO
    {
        [Required, Key]
        public Guid Id { get; set; }
        [Required]
        public List<Guid> Genres { get; set; } = new List<Guid>();

    }
}
