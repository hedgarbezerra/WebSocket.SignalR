using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace WebSocket.SignalR.Data.ModelBuilders
{
    public class MovieBuilder : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movies");
            builder.HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.DirectorName).HasMaxLength(255).IsRequired();
            builder.Property(p => p.Classification).HasMaxLength(255).IsRequired();
            builder.Property(p => p.Release).IsRequired();
            builder.Property(p => p.Sinopsys).IsRequired().HasColumnType("varchar(max)");
            builder.Property(p => p.Duration).HasConversion<TimeSpanToTicksConverter>();
            builder.Property(p => p.Starring).HasConversion(to => string.Join(",", to), from => from.Split(",", StringSplitOptions.TrimEntries).ToList());

            builder.HasMany(m => m.Genres)
               .WithMany(g => g.Movies);

               //.UsingEntity<Dictionary<string, object>>(
               //    "MovieGenre",
               //    j => j.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
               //    j => j.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
               //    j =>
               //    {
               //        j.Property<Guid>("MovieId");
               //        j.Property<Guid>("GenreId");
               //        j.HasKey("MovieId", "GenreId");
               //    });

        }
    }

    public class GenreBuilder : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genres");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
