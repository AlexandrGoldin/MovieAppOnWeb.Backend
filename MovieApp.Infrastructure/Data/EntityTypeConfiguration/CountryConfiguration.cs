using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Infrastructure.Entities;

namespace MovieApp.Infrastructure.Data.EntityTypeConfiguration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
               .UseHiLo("movie_country_hilo")
               .IsRequired();

            builder.Property(c => c.CountryName)
                .IsRequired()
                .HasMaxLength(30);
        }
    }
}
