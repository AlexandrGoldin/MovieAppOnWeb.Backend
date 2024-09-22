using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.ApplicationCore.Constants;
using MovieApp.Infrastructure.Features.Movies.Commands.CreateMovie;
using MovieApp.Infrastructure.Features.Movies.Commands.DeleteMovie;
using MovieApp.Infrastructure.Features.Movies.Commands.UpdateMovie;
using MovieApp.Infrastructure.Features.Movies.Queries;
using MovieApp.Infrastructure.Movies.Queries.GetMovieDetails;
using MovieApp.Infrastructure.Movies.Queries.GetMovieList;

namespace MovieApp.WebApi.Endpoints
{
    public static class MovieEndpointsgit
    {
        public static void MapMovieEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/movies", async (
                  [FromQuery] string? searchTerm,
                  [FromQuery] string? primaryReleaseYear,
                  [FromQuery] string? withGenres,
                  [FromQuery] string? withCountries,
                  [FromQuery] string? sortBy,
                  [FromQuery] int page,
                  [FromQuery] int pageSize,
                  ISender sender) =>
          {
              var queryParams = new MovieQueryParams
              {
                  SearchTerm = searchTerm,
                  PrimaryReleaseYear = primaryReleaseYear,
                  WithGenres = withGenres,
                  WithCountries = withCountries,
                  SortBy = sortBy,
                  Page = page,
                  PageSize = pageSize
              };

              var query = new GetMovieListQuery(queryParams);
              var movieListResponse = await sender.Send(query);
              return Results.Ok(movieListResponse);
          });

            app.MapGet("/api/movies/{id}",
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

            app.MapPut("/api/movies/{id}",
                [Authorize(Roles = Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            async (UpdateMovieCommand comand, ISender sender) =>           
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
