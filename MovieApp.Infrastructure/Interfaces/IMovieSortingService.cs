using MovieApp.Infrastructure.Features.Movies.Queries;

namespace MovieApp.Infrastructure.Interfaces
{
    internal interface IMovieSortingService
    {
        Task<List<MovieQueryResponse>> SortingMoviesAsync(MovieQueryParams queryParams,
            List<MovieQueryResponse> movieList);
    }
}
