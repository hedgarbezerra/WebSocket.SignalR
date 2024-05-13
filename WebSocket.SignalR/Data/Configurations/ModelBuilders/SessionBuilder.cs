using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebSocket.SignalR.Models;

namespace WebSocket.SignalR.Data.Configurations.ModelBuilders
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

            builder.HasMany(p => p.SeatsTaken)
                .WithOne(p => p.Session)
                .HasForeignKey(p => p.SessionId);

            builder.HasOne(p => p.Movie)
                .WithMany()
                .HasForeignKey(p => p.MovieId);

            builder.HasOne(p => p.Room)
                .WithMany()
                .HasForeignKey(p => p.RoomId);

            builder.Navigation(p => p.Room).EnableLazyLoading().AutoInclude();
            builder.Navigation(p => p.Movie).EnableLazyLoading().AutoInclude();
            builder.Navigation(p => p.SeatsTaken).EnableLazyLoading().AutoInclude();

            builder.Ignore(p => p.IsFull);
        }
    }
}
