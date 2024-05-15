using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebSocket.SignalR.Models;

namespace WebSocket.SignalR.Data.Configurations.ModelBuilders
{
    public class RoomBuilder : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(255);
            builder.Property(p => p.Type).HasConversion<int>();
            builder.Navigation(p => p.Seats).EnableLazyLoading().AutoInclude();

            builder.Ignore(p => p.IsEmpty);
            builder.Ignore(p => p.SeatCount);
        }
    }
}