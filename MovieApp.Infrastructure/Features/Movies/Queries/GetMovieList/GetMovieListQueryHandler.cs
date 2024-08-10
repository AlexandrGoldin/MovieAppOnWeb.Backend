using MediatR;
using MovieApp.Infrastructure.Entities;
using MovieApp.Infrastructure.Features.Movies.Queries;
using MovieApp.Infrastructure.Interfaces;
using MovieApp.Infrastructure.Specifications;
using System.Linq.Expressions;

namespace MovieApp.Infrastructure.Movies.Queries.GetMovieList
{
    internal sealed class GetMovieListQueryHandler :
        IRequestHandler<GetMovieListQuery, PagedList<MovieQueryResponse>>
    {
        private readonly IUriComposer _uriComposer;
        private readonly IReadRepository<Movie> _movieRepository;

        public GetMovieListQueryHandler(IReadRepository<Movie> movieRepository,
            IUriComposer uriComposer)
        {
            _uriComposer = uriComposer;
            _movieRepository = movieRepository;
        }

        public async Task<PagedList<MovieQueryResponse>> Handle(GetMovieListQuery request, 
            CancellationToken cancellationToken)
        {
            await Task.Delay(1000);

            var queryParams = request.QueryParams;

            var spec = new MovieListFilterWithCountryAndGenreSpec(queryParams);

            var movieList = (await _movieRepository.ListAsync(spec, cancellationToken));

            movieList = GetMovieListFilteredByGenreAndCountry(queryParams, movieList);

            movieList = GetSortedListOfMovies(queryParams, movieList);

            var movieResponses = movieList!
                .Select(m => new MovieQueryResponse
                {
                    MovieId = m.Id,
                    Title = m.Title!,
                    Overview = m.Overview!,
                    Description = m.Description!,
                    Price = m.Price,
                    PictureUri = _uriComposer.ComposePicUri(m.PictureUri!),
                    Audience = m.Audience!,
                    Rating = m.Rating,
                    ReleaseDate = m.ReleaseDate,
                    CountryName = m.Country!.CountryName,
                    GenreName = m.Genre!.GenreName
                });

            var movies = PagedList<MovieQueryResponse>.CreateAsync(
                movieResponses.ToList(),
                queryParams.Page,
                queryParams.PageSize);
            return movies;
        }

        private static List<Movie>? GetMovieListFilteredByGenreAndCountry(MovieQueryParams queryParams, List<Movie>? movieList)
        {
            if (!string.IsNullOrWhiteSpace(queryParams.WithCountries)
                && !string.IsNullOrWhiteSpace(queryParams.WithGenres))
            {
                string[] stringArrayCountry = queryParams.WithCountries.Split(',');
                var arrayCountryIds = Array.ConvertAll(stringArrayCountry, int.Parse);

                movieList = movieList!.Where(m => arrayCountryIds.Contains(m.CountryId)).ToList();
            }

            return movieList;
        }

        private static List<Movie>? GetSortedListOfMovies(MovieQueryParams queryParams, List<Movie>? movieList)
        {
            string sortColumn, sortOrder;
            GetMovieSortingDirection(queryParams, out sortColumn, out sortOrder);

            movieList = SortingMovies(movieList, sortColumn, sortOrder);
            return movieList;
        }

        private static List<Movie>? SortingMovies(List<Movie>? movieList, string sortColumn, string sortOrder)
        {
            if (sortOrder.ToLower() == "desc")
            {
                movieList = movieList?.AsQueryable().OrderByDescending(GetFieldToSortMovies(sortColumn))
                    .ToList();
            }
            else
            {
                movieList = movieList?.AsQueryable().OrderBy(GetFieldToSortMovies(sortColumn))
                    .ToList();
            }

            return movieList;
        }

        private static void GetMovieSortingDirection(MovieQueryParams queryParams, out string sortColumn, out string sortOrder)
        {
            sortColumn = "rating";
            sortOrder = "desc";
            if (!string.IsNullOrWhiteSpace(queryParams.SortBy))
            {
                var sortParams = queryParams.SortBy.Split('.');
                if (sortParams.Length == 2)
                {
                    sortColumn = sortParams[0];
                    sortOrder = sortParams[1];
                }
            }
        }

        private static Expression<Func<Movie, object>> GetFieldToSortMovies(string sortColumn)
            => sortColumn.ToLower() switch
            {
                "release_date" => movie => movie.ReleaseDate!,
                "rating" => movie => movie.Rating,
                _ => movie => movie.Rating
            };
    }
}


