using Microsoft.EntityFrameworkCore;

namespace WebSocket.SignalR.Data.ModelBuilders.Seeding
{
    public class RoomsSeeds
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<Room>()
                .HasData(
                    new Room { Id = Guid.NewGuid(), Type = Enumerations.ERoomType.Standard, Seats = new List<Seat>
                    {
                        new Seat { Id = Guid.NewGuid(), Row = 0, Column = 0, IsHandicapAccessible = true  }
                    }
                });

        }
    }
}
