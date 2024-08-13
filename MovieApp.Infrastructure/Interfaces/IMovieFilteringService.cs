using MovieApp.Infrastructure.Entities;
using MovieApp.Infrastructure.Features.Movies.Queries;

namespace MovieApp.Infrastructure.Interfaces
{
    public interface IMovieFilteringService
    {
        //Task<List<Movie>> FilterMoviesAsyncByDateAndTwoCheckboxes(MovieQueryParams queryParams, CancellationToken cancellationToken);
        Task<List<Movie>> FilteringMoviesAsync(MovieQueryParams queryParams, CancellationToken cancellationToken);
    }
}
