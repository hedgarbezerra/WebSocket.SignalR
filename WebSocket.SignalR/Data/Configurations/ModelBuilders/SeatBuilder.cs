using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebSocket.SignalR.Models;

namespace WebSocket.SignalR.Data.Configurations.ModelBuilders
{
    public class SeatBuilder : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.ToTable("RoomSeats");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.IsHandicapAccessible)
                .HasColumnType("bit");
            builder.Property(p => p.Row)
                .HasColumnType("integer").IsRequired();
            builder.Property(p => p.Column)
                .HasColumnType("integer").IsRequired();

            builder.HasOne(p => p.Room)
                .WithMany(p => p.Seats)
                .HasForeignKey(p => p.RoomId);
        }
    }

    public class SeatTakenBuilder : IEntityTypeConfiguration<SeatTaken>
    {
        public void Configure(EntityTypeBuilder<SeatTaken> builder)
        {
            builder.ToTable("SessionTakenRoomSeats");
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Seat).WithMany()
                .HasForeignKey(p => p.SeatId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
