using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebSocket.SignalR.Models;

namespace WebSocket.SignalR.Data.Configurations.ModelBuilders
{
    public class AppUserBuilder : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(p => p.Birthdate).IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(255);

            builder.HasMany(p => p.SeatsTaken)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);
        }
    }
}
