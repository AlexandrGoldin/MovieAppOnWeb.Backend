using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieApp.ApplicationCore.Entities;
using MovieApp.Infrastructure.Identity;
using System.Reflection;

namespace MovieApp.Infrastructure.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
#pragma warning disable CS8618 // Required by Entity Framework
        public DbSet<Order> Orders { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public ApplicationContext()
        { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<ApplicationUser>(b =>
            {
                b.Property(u => u.AvatarUri).HasMaxLength(100);
            });
        }
    }
}
