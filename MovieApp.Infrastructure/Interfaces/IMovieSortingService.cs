using MovieApp.Infrastructure.Entities;
using MovieApp.Infrastructure.Features.Movies.Queries;

namespace MovieApp.Infrastructure.Interfaces
{
    internal interface IMovieSortingService
    {
        Task<List<Movie>> SortingMoviesAsync(MovieQueryParams queryParams,
            List<Movie> movieList);
    }
}
