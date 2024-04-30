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
            builder.HasMany(q => q.Seats)
                .WithOne(p => p.Room)
                .HasForeignKey(p => p.RoomId);

            builder.Ignore(p => p.IsEmpty);
            builder.Ignore(p => p.SeatCount);
        }
    }
}

//w =>
//{
//    w.ToTable("tbCustomDictionaryWord");
//    w.HasKey(wo => wo.Id);
//    w.WithOwner()
//    .HasForeignKey(x => x.IdDictionary);

//    w.Property(wp => wp.Word).HasColumnType("varchar(50)");
//}