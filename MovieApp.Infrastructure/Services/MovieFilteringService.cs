using AutoMapper;
using MovieApp.Infrastructure.Entities;
using MovieApp.Infrastructure.Features.Movies.Queries;
using MovieApp.Infrastructure.Interfaces;
using MovieApp.Infrastructure.Specifications;

namespace MovieApp.Infrastructure.Services
{
    public class MovieFilteringService : IMovieFilteringService
    {
        private readonly IReadRepository<Movie> _movieRepository;
        private readonly IUriComposer _uriComposer;
        private readonly IMapper _mapper;

        public MovieFilteringService
            (IUriComposer uriComposer, IReadRepository<Movie> movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _uriComposer = uriComposer;
            _mapper = mapper;
        }

        public async Task<List<MovieQueryResponse>> FilteringMoviesAsync(MovieQueryParams queryParams,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(queryParams.WithGenres)
                    || string.IsNullOrWhiteSpace(queryParams.WithCountries))
            {
                var spec = new MovieListFilterWithCountryAndGenreSpec(queryParams);

                var movieList = await _movieRepository.ListAsync(spec, cancellationToken);

                return MappingMovieListToMovieQueryResponseList(movieList);
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

            var movieListFilterByGenres
                = MappingMovieListToMovieQueryResponseList(await _movieRepository.ListAsync(specForGenre, cancellationToken));

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

            var movieListFilterByCountries
                = MappingMovieListToMovieQueryResponseList(await _movieRepository.ListAsync(specForCountries));

            var movieListRes = movieListFilterByGenres.
               Intersect(movieListFilterByCountries).ToList();

            return movieListRes;
        }

        private List<MovieQueryResponse> MappingMovieListToMovieQueryResponseList(List<Movie> movieList)
        {
            var movieQueryResponseList = movieList!
                .Select(_mapper.Map<MovieQueryResponse>).ToList();
            foreach (MovieQueryResponse movie in movieQueryResponseList) 
            {
                movie.PictureUri = _uriComposer.ComposePicUri(movie.PictureUri!);
            }

           return movieQueryResponseList;    
        }
    }
}
