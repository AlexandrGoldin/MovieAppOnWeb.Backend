using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Infrastructure.Common.Behaviors;
using MovieApp.Infrastructure.Data;
using MovieApp.Infrastructure.Interfaces;
using System.Reflection;

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
            services.AddValidatorsFromAssemblies(new[] {Assembly.GetExecutingAssembly()});

            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssembly(assembly);

            string? connection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(connection));

            services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            
            return services;
        }
    }
}
