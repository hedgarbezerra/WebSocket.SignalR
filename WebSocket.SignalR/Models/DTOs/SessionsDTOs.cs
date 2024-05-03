using System.ComponentModel.DataAnnotations;

namespace WebSocket.SignalR.Models.DTOs
{
    public record CreateSessionDTO
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
    public record UpdateSessionDTO : CreateSessionDTO
    {
        [Required, Key]
        public Guid Id { get; set; }
    }

    public record AssignSeatToUserDTO
    {
        [Required]
        public Guid SessionId { get; set; }
        [Required]
        public Guid SeatId { get; set; }
    }
    public record AssignMultipleSeatsToUserDTO
    {
        [Required]
        public Guid SessionId { get; set; }
        [Required]
        public List<Guid> SeatsIds { get; set; }
    }
}
