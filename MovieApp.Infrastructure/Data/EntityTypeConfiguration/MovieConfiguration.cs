using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.ApplicationCore.Entities;

namespace MovieApp.Infrastructure.Data.EntityTypeConfiguration
{
    internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movies");

            builder.Property(m => m.Id)
                .UseHiLo("movie_hilo")
                .IsRequired();

            builder.Property(m => m.Title)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(m => m.Overview)
               .IsRequired(true)
               .HasMaxLength(150);

            builder.Property(cm => cm.Description)
               .IsRequired(true)
               .HasMaxLength(300);

            builder.Property(m => m.Audience)
                .IsRequired(true)
                .HasMaxLength(20);

            builder.Property(cm => cm.PictureUri)
                .IsRequired(false);

            builder.Property(m => m.Rating)
                 .IsRequired(true)
                 .HasMaxLength(10);

            builder.Property(m => m.ReleaseDate)
                 .IsRequired(true);

            builder.HasOne(m => m.Country)
                .WithMany(m => m.Movies)
                .HasForeignKey(m => m.CountryId)
                .IsRequired(); 

            builder.HasOne(m => m.Genre)
                .WithMany(m => m.Movies)
                .HasForeignKey(m => m.GenreId)
                .IsRequired(); 
        }
    }
}
