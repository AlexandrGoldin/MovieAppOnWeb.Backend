using MediatR;
using MovieApp.Infrastructure.Features.Genres.Queries.GetGenreList;

namespace MovieApp.WebApi.Endpoints
{
    public static class GenresEndpoints
    {
        public static void MapGenreEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/genres", async (ISender sender) =>
            {
                var query = new GetGenresListQuery();
                var genreListResponse = await sender.Send(query);

                return Results.Ok(genreListResponse);
            });
        }
    }
}
