using MediatR;
using MovieApp.Infrastructure.Entities;
using MovieApp.Infrastructure.Features.Movies.Queries;
using MovieApp.Infrastructure.Interfaces;
using MovieApp.Infrastructure.Specifications;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

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

            if (TryParseDateRange(request.SearchTerm, out var minDate, out var maxDate))
            {
                request = request with { MinDate = minDate, MaxDate = maxDate };
            }

            var spec = request.MinDate.HasValue && request.MaxDate.HasValue
              ? new MovieListFilterWithCountryAndGenreSpec(request.MinDate.Value, request.MaxDate.Value)
              : new MovieListFilterWithCountryAndGenreSpec(request.SearchTerm);

            var movieList = (await _movieRepository.ListAsync(spec, cancellationToken));

            if (request.SortOrder?.ToLower() == "desc")
            {
                movieList = movieList?.AsQueryable().OrderByDescending(SortForMovies(request))
                    .ToList();
            }
            else
            {
                movieList = movieList?.AsQueryable().OrderBy(SortForMovies(request))
                    .ToList();
            }

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
                request.Page,
                request.PageSize);
            return movies;
        }

        private static Expression<Func<Movie, object>> SortForMovies(GetMovieListQuery request)
            => request.SortColumn?.ToLower() switch
            {
                "releaseDate" => movie => movie.ReleaseDate!,
                "rating" => movie => movie.Rating,
                _ => movie => movie.Id
            };


        private static bool TryParseDateRange(string? searchTerm, out int minDate, out int maxDate)
        {
            minDate = maxDate = 0;
            if (string.IsNullOrWhiteSpace(searchTerm)) return false;

            var regex = new Regex(@"^(19|20)\d{2},\s*(19|20)\d{2}$");
            var match = regex.Match(searchTerm);
            if (!match.Success) return false;

            var dates = searchTerm.Split(',').Select(s => int.Parse(s.Trim())).ToArray();
            if (dates[0] < dates[1])
            {
                minDate = dates[0];
                maxDate = dates[1];
                return true;
            }

            return false;
        }
    }
}
