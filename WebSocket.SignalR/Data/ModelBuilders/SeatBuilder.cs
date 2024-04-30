using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebSocket.SignalR.Data.ModelBuilders
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
        }
    }

    public class SeatTakenBuilder : IEntityTypeConfiguration<SeatTaken>
    {
        public void Configure(EntityTypeBuilder<SeatTaken> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id);
            builder.Property(p => p.IsHandicapAccessible)
                .HasColumnType("bit");
            builder.Property(p => p.Row)
                .HasColumnType("integer").IsRequired();
            builder.Property(p => p.Column)
                .HasColumnType("integer").IsRequired();
        }
    }
}
