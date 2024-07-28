using Carter;
using MediatR;
using MovieApp.Infrastructure.Features.Genres.Queries.GetGenreList;

namespace MovieApp.WebApi.Endpoints
{
    public class Genres : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
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
