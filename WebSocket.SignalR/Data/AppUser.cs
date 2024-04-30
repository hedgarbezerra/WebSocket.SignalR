using Microsoft.AspNetCore.Identity;

namespace WebSocket.SignalR.Data
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }

        public virtual List<SeatTaken> SeatsTaken { get; set; } = new List<SeatTaken>();
    }
}
