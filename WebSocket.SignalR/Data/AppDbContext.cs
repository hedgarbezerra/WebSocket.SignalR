using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebSocket.SignalR.Data.Configurations.ModelBuilders.Seeding;
using WebSocket.SignalR.Data.Configurations.ModelBuilders;
using WebSocket.SignalR.Models;

namespace WebSocket.SignalR.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MovieBuilder());
            builder.ApplyConfiguration(new GenreBuilder());
            builder.ApplyConfiguration(new RoomBuilder());
            builder.ApplyConfiguration(new SeatBuilder());
            builder.ApplyConfiguration(new SeatTakenBuilder());
            builder.ApplyConfiguration(new SessionBuilder());

            base.OnModelCreating(builder);

            RoomsSeeds.Seed(builder);
            GenresSeed.Seed(builder);
        }
        DbSet<Movie> Movies { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<Session> Sessions { get; set; }
    }
}
