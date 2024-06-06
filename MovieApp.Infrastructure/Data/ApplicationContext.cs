using Microsoft.EntityFrameworkCore;
using MovieApp.Infrastructure.Entities;
using System.Reflection;

namespace MovieApp.Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
#pragma warning disable CS8618 // Required by Entity Framework
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
        }
    }
}
