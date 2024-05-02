using System.ComponentModel.DataAnnotations;

namespace WebSocket.SignalR.Models
{
    public record PaginationInput
    {
        [Required]
        public int Index { get; set; } = 1;
        [Required]
        public int Size { get; set; } = 10;
        [MaxLength(255)]
        public string SearchTerm { get; set; } = string.Empty;
    }
}
