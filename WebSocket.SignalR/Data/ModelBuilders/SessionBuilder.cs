using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebSocket.SignalR.Data.ModelBuilders
{
    public class SessionBuilder : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Sessions");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Date)
                .HasColumnType("datetime")
                .IsRequired();
            builder.Property(p => p.Language)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(p => p.SeatsTaken).WithOne(p => p.Session).HasForeignKey(p => p.SessionId);
            builder.HasOne(p => p.Movie).WithMany(p => p.Sessions)
                .HasForeignKey(p => p.MovieId);
            builder.HasOne(p => p.Room).WithMany(p => p.Sessions)
                .HasForeignKey(p => p.MovieId);

            builder.Ignore(p => p.IsFull);
        }
    }
}
