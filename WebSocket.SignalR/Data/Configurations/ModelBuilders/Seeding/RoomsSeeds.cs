using Microsoft.EntityFrameworkCore;
using WebSocket.SignalR.Models;

namespace WebSocket.SignalR.Data.Configurations.ModelBuilders.Seeding
{
    public class RoomsSeeds
    {
        public static void Seed(ModelBuilder builder)
        {
            Guid firstRoomId = Guid.NewGuid();
            builder.Entity<Room>().HasData(new Room { Id = firstRoomId, Type = Enumerations.ERoomType.Standard, Name = "Sala padrão 2D" });

            builder.Entity<Seat>().HasData(
                new Seat { Id = Guid.NewGuid(), Row = 0, Column = 0, IsHandicapAccessible = false, RoomId = firstRoomId },
                new Seat { Id = Guid.NewGuid(), Row = 0, Column = 1, IsHandicapAccessible = false, RoomId = firstRoomId },
                new Seat { Id = Guid.NewGuid(), Row = 0, Column = 2, IsHandicapAccessible = false, RoomId = firstRoomId },
                new Seat { Id = Guid.NewGuid(), Row = 0, Column = 3, IsHandicapAccessible = false, RoomId = firstRoomId },
                new Seat { Id = Guid.NewGuid(), Row = 1, Column = 0, IsHandicapAccessible = false, RoomId = firstRoomId },
                new Seat { Id = Guid.NewGuid(), Row = 1, Column = 1, IsHandicapAccessible = false, RoomId = firstRoomId },
                new Seat { Id = Guid.NewGuid(), Row = 1, Column = 2, IsHandicapAccessible = false, RoomId = firstRoomId },
                new Seat { Id = Guid.NewGuid(), Row = 1, Column = 3, IsHandicapAccessible = false, RoomId = firstRoomId },
                new Seat { Id = Guid.NewGuid(), Row = 2, Column = 0, IsHandicapAccessible = false, RoomId = firstRoomId },
                new Seat { Id = Guid.NewGuid(), Row = 2, Column = 1, IsHandicapAccessible = true, RoomId = firstRoomId },
                new Seat { Id = Guid.NewGuid(), Row = 2, Column = 2, IsHandicapAccessible = true, RoomId = firstRoomId },
                new Seat { Id = Guid.NewGuid(), Row = 2, Column = 3, IsHandicapAccessible = false, RoomId = firstRoomId }
                );


            Guid iMaxRoomId = Guid.NewGuid();
            builder.Entity<Room>().HasData(new Room { Id = iMaxRoomId, Type = Enumerations.ERoomType.IMAX, Name = "Sala Pequena IMAX" });
            builder.Entity<Seat>().HasData(
                new Seat { Id = Guid.NewGuid(), Row = 0, Column = 0, IsHandicapAccessible = false, RoomId = iMaxRoomId },
                new Seat { Id = Guid.NewGuid(), Row = 0, Column = 1, IsHandicapAccessible = false, RoomId = iMaxRoomId },
                new Seat { Id = Guid.NewGuid(), Row = 0, Column = 2, IsHandicapAccessible = false, RoomId = iMaxRoomId },
                new Seat { Id = Guid.NewGuid(), Row = 0, Column = 3, IsHandicapAccessible = false, RoomId = iMaxRoomId },
                new Seat { Id = Guid.NewGuid(), Row = 1, Column = 0, IsHandicapAccessible = false, RoomId = iMaxRoomId },
                new Seat { Id = Guid.NewGuid(), Row = 1, Column = 1, IsHandicapAccessible = false, RoomId = iMaxRoomId },
                new Seat { Id = Guid.NewGuid(), Row = 1, Column = 2, IsHandicapAccessible = true, RoomId = iMaxRoomId },
                new Seat { Id = Guid.NewGuid(), Row = 1, Column = 3, IsHandicapAccessible = true, RoomId = iMaxRoomId }
                );
        }
    }
}
