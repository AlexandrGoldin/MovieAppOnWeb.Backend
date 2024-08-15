using MovieApp.Infrastructure.Features.Movies.Queries;

namespace MovieApp.Infrastructure.Interfaces
{
    public interface IMovieFilteringService
    {
        Task<List<MovieQueryResponse>> FilteringMoviesAsync(MovieQueryParams queryParams, 
            CancellationToken cancellationToken);
    }
}
