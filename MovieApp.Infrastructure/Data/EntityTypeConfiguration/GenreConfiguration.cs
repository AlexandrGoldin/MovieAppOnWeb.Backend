using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.ApplicationCore.Entities;

namespace MovieApp.Infrastructure.Data.EntityTypeConfiguration
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
               .UseHiLo("movie_genre_hilo")
               .IsRequired();

            builder.Property(g => g.GenreName)
                .IsRequired()
                .HasMaxLength(30);
        }
    }
}
