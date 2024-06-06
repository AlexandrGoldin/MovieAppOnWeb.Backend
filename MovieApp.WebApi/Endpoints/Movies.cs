using Carter;
using MediatR;
using MovieApp.Infrastructure.Movies.Queries.GetMovieDetails;
using MovieApp.Infrastructure.Movies.Queries.GetMovieList;

namespace MovieApp.WebApi.Endpoints
{
    public class Movies : ICarterModule
    {

        public async void AddRoutes(IEndpointRouteBuilder app)
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
                var movies = await sender.Send(query);
                return Results.Ok(movies);
            });

            app.MapGet("/api/movies/{id}",
                async (int id, ISender sender) =>
                {
                    return Results.Ok(await sender.Send(new GetMovieDetailsQuery(id)));
                });
        }
    }

}
