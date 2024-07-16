using Carter;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using MovieApp.ApplicationCore.Constants;
using MovieApp.Infrastructure.Features.Movies.Commands.CreateMovie;
using MovieApp.Infrastructure.Features.Movies.Commands.DeleteMovie;
using MovieApp.Infrastructure.Features.Movies.Commands.UpdateMovie;
using MovieApp.Infrastructure.Movies.Queries.GetMovieDetails;
using MovieApp.Infrastructure.Movies.Queries.GetMovieList;

namespace MovieApp.WebApi.Endpoints
{
    public class Movies : ICarterModule
    {

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/movies", async (
                string? searchTerm,
                string? sortColumn,
                string? sortOrder,
                int page,
                int pageSize,
                ISender sender) =>
            {
                var query = new GetMovieListQuery(searchTerm, sortColumn, 
                    sortOrder, page, pageSize);
                var movieListResponse = await sender.Send(query);
                return Results.Ok(movieListResponse);
            });

            app.MapGet("/api/movies/{id}",
                [Authorize]
                async (int id, ISender sender) =>
                {
                    return Results.Ok(await sender.Send(new GetMovieDetailsQuery(id)));
                });

            app.MapPost("/api/movies",
                [Authorize(Roles = Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            async (CreateMovieCommand comand, ISender sender) =>
            {
                var movieResponse = await sender.Send(comand);

                return Results.Ok(movieResponse.MovieId);
            })
              .DisableAntiforgery(); 

            app.MapPut("/api/movies/{id}", async (UpdateMovieCommand comand, ISender sender) =>
            {
                var movieResponse = await sender.Send(comand);

                return Results.Ok(movieResponse.MovieId);
            });

            app.MapDelete("/api/movies/{id}",
                 [Authorize(Roles = Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            async (int id, ISender sender) =>
            {
                await sender.Send(new DeleteMovieCommand(id));

                return Results.NoContent();   
            })
            .WithOpenApi();
        }
    }

}
