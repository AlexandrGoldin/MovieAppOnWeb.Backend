using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Infrastructure.Features.Movies.Queries;
using MovieApp.Infrastructure.Movies.Queries.GetMovieList;

namespace MovieApp.WebApi.Endpoints
{
    public static class MovieEndpoints
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
        }
    }
}
