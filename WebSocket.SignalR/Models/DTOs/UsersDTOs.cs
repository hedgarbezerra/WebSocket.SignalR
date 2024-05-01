using System.ComponentModel.DataAnnotations;

namespace WebSocket.SignalR.Models.DTOs
{
    public class UserRegisterDTO
    {
        [Required, MaxLength(500)]
        public string Email { get; set; }
        [Required, MaxLength(255)]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
    }
}
