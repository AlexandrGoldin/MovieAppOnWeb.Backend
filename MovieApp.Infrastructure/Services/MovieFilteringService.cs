using MovieApp.Infrastructure.Entities;
using MovieApp.Infrastructure.Features.Movies.Queries;
using MovieApp.Infrastructure.Interfaces;
using MovieApp.Infrastructure.Specifications;

namespace MovieApp.Infrastructure.Services
{
    public class MovieFilteringService : IMovieFilteringService
    {
        private readonly IReadRepository<Movie> _movieRepository;

        public MovieFilteringService(IReadRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<List<Movie>> FilteringMoviesAsync(MovieQueryParams queryParams,
            CancellationToken cancellationToken)
        {
            var movieList = new List<Movie>();
            if (string.IsNullOrWhiteSpace(queryParams.WithGenres)
                    || string.IsNullOrWhiteSpace(queryParams.WithCountries))
            {
                var spec = new MovieListFilterWithCountryAndGenreSpec(queryParams);

                movieList = await _movieRepository.ListAsync(spec, cancellationToken);
                return movieList;
            }
            
                var queryParamsWithGenres =
                    new MovieQueryParams
                    {
                        SearchTerm = queryParams.SearchTerm,
                        PrimaryReleaseYear = queryParams.PrimaryReleaseYear,
                        WithGenres = queryParams.WithGenres,
                        WithCountries = "",
                        SortBy = queryParams.SortBy,
                        Page = queryParams.Page,
                        PageSize = queryParams.PageSize,
                    };

            var specForGenre =
                new MovieListFilterWithCountryAndGenreSpec(queryParamsWithGenres);

            var movieListFilterByGenres =
                await _movieRepository.ListAsync(specForGenre, cancellationToken);

            var queryParamsWithCountries = new MovieQueryParams
            {
                SearchTerm = queryParams.SearchTerm,
                PrimaryReleaseYear = queryParams.PrimaryReleaseYear,
                WithGenres = "",
                WithCountries = queryParams.WithCountries,
                SortBy = queryParams.SortBy,
                Page = queryParams.Page,
                PageSize = queryParams.PageSize,
            };

            var specForCountries =
                new MovieListFilterWithCountryAndGenreSpec(queryParamsWithCountries);

            var movieListFilterByCountries = (await _movieRepository
                .ListAsync(specForCountries));

            movieList = movieListFilterByGenres.
                Intersect(movieListFilterByCountries).ToList();

            return movieList;
        }
    }
}
