using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebSocket.SignalR.Data.ModelBuilders;

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

            base.OnModelCreating(builder);
        }
        DbSet<Movie> Movies { get; set; }
    }
}
