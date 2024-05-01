using Microsoft.EntityFrameworkCore;
using WebSocket.SignalR.Models;

namespace WebSocket.SignalR.Data.Configurations.ModelBuilders.Seeding
{
    public class GenresSeed
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<Genre>()
                .HasData(
                    new Genre { Id = Guid.NewGuid(), Name = "Ação" },
                    new Genre { Id = Guid.NewGuid(), Name = "Comédia" },
                    new Genre { Id = Guid.NewGuid(), Name = "Romance" },
                    new Genre { Id = Guid.NewGuid(), Name = "Terror" },
                    new Genre { Id = Guid.NewGuid(), Name = "Thriller" },
                    new Genre { Id = Guid.NewGuid(), Name = "Aventura" },
                    new Genre { Id = Guid.NewGuid(), Name = "Animação" }
                );
        }
    }
}
