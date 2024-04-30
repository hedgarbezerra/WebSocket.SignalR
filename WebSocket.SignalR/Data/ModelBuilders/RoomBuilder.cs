using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebSocket.SignalR.Data.ModelBuilders
{
    public class RoomBuilder : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(255);
            builder.Property(p => p.Type).HasConversion<int>();

            builder.Ignore(p => p.IsEmpty);
            builder.Ignore(p => p.SeatCount);
        }
    }
}

//builder.OwnsMany(q => q.Seats, s =>
//{
//    s.ToTable("RoomSeats");
//    s.HasKey(p => p.Id);

//    s.Property(p => p.IsHandicapAccessible)
//        .HasColumnType("bit");
//    s.Property(p => p.Row)
//        .HasColumnType("integer").IsRequired();
//    s.Property(p => p.Column)
//        .HasColumnType("integer").IsRequired();

//    s.WithOwner(p => p.Room).HasForeignKey(p => p.RoomId);
//});