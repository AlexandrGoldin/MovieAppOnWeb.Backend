using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Infrastructure;
using MovieApp.Infrastructure.Interfaces;
using MovieApp.Infrastructure.Services;
using MovieApp.Infrastructure.Data;
using FluentValidation;

namespace MovieApp.Infrastructure
{
    public static class ConfigureInfrastructureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
             IConfiguration configuration)
        {
            var assembly = typeof(ConfigureInfrastructureServices).Assembly;

            services.AddMediatR(configuration =>
                 configuration.RegisterServicesFromAssembly(assembly));

            services.AddValidatorsFromAssembly(assembly);

           // return services;

            string? connection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(connection));

            services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            
            //var catalogSettings = configuration.Get<CatalogSettings>() ?? new CatalogSettings();
            //services.AddSingleton<IUriComposer>(new UriComposer(catalogSettings));


            return services;
        }
    }
}
