using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MinimalApi.Endpoint.Extensions;
using MovieApp.ApplicationCore.Constants;
using MovieApp.ApplicationCore.Interfaces;
using MovieApp.Infrastructure;
using MovieApp.Infrastructure.Data;
using MovieApp.Infrastructure.Identity;
using MovieApp.Infrastructure.Interfaces;
using MovieApp.Infrastructure.Services;
using MovieApp.WebApi;
using MovieApp.WebApi.Middleware;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints();

builder.Services.AddCarter();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationContext>()
        .AddDefaultTokenProviders();

var catalogSettings = builder.Configuration.Get<CatalogSettings>() ?? new CatalogSettings();
builder.Services.AddSingleton<IUriComposer>(new UriComposer(catalogSettings));
builder.Services.AddScoped<ITokenClaimsService, IdentityTokenClaimService>();

var key = Encoding.ASCII.GetBytes(AuthorizationConstants.JWT_SECRET_KEY);
builder.Services.AddAuthentication(config =>
{
    config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddEndpoints();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddAntiforgery();

builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => {
options.MapType<DateOnly>(() => new OpenApiSchema
{
    Type = "string",
    Format = "date"
});
options.EnableAnnotations();
options.SchemaFilter<CustomSchemaFilters>();
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer"
});
options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
            });
});

var app = builder.Build();

app.UseSerilogRequestLogging();

app.Logger.LogInformation("App created...");

app.Logger.LogInformation("Seeding Database...");

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var applicationContext = scopedProvider.GetRequiredService<ApplicationContext>();
        await ApplicationContextSeed.SeedAsync(applicationContext, app.Logger);

        var userManager = scopedProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await AppIdentityDbContextSeed.SeedAsync(applicationContext, userManager, roleManager);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });

}

app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.MapCarter();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(builder => builder.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapEndpoints();

app.Run();

public partial class Program { }