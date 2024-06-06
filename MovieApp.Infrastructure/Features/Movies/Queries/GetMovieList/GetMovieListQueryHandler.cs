using MediatR;
using MovieApp.Infrastructure.Entities;
using MovieApp.Infrastructure.Interfaces;
using MovieApp.Infrastructure.Movies.Queries.GetMovieDetails;
using MovieApp.Infrastructure.Specifications;
using System.Linq.Expressions;

namespace MovieApp.Infrastructure.Movies.Queries.GetMovieList
{
    internal sealed class GetMovieListQueryHandler :
        IRequestHandler<GetMovieListQuery, PagedList<MovieResponse>>
    {
        private readonly IUriComposer _uriComposer;
        private readonly IReadRepository<Movie> _movieRepository;

        public GetMovieListQueryHandler(IReadRepository<Movie> movieRepository,
            IUriComposer uriComposer)
        {
            _uriComposer = uriComposer;
            _movieRepository = movieRepository;
        }

        public async Task<PagedList<MovieResponse>> Handle(GetMovieListQuery request,
            CancellationToken cancellationToken)
        {
            await Task.Delay(1000);

            var spec = new MovieListWithCountryAndGenreSpec();

            var movieList = (await _movieRepository.ListAsync(spec,
                cancellationToken));

            movieList = SearchForMovies(request, movieList);

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
                .Select(m => new MovieResponse
                //return moviesQuery!.Select(m => new MovieResponse 
                {
                    Id = m.Id,
                    Title = m.Title,
                    Overview = m.Overview,
                    Description = m.Description,
                    PictureUri = _uriComposer.ComposePicUri(m.PictureUri!),
                    Audience = m.Audience,
                    Rating = m.Rating,
                    ReleaseDate = m.ReleaseDate,
                    CountryName = m.Country!.CountryName,
                    GenreName = m.Genre!.GenreName
                }).ToList();

            var movies = PagedList<MovieResponse>.CreateAsync(
                movieResponses,
                request.Page,
                request.PageSize);

            return movies;//movieResponses;
        }

        private static Expression<Func<Movie, object>> SortForMovies(GetMovieListQuery request)
            => request.SortColumn?.ToLower() switch
            {
                "releaseDate" => movie => movie.ReleaseDate!,
                "rating" => movie => movie.Rating,
                _ => movie => movie.Id
            };

        private static List<Movie>? SearchForMovies(GetMovieListQuery request, IEnumerable<Movie>? movieList)
        {
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                movieList = movieList!.Where(m =>
                m.Title!.ToLower().Contains(request.SearchTerm.ToLower()) ||
                ((string)m.Country!).ToLower().Contains(request.SearchTerm.ToLower()) ||
                ((string)m.Genre!).ToLower().Contains(request.SearchTerm.ToLower()));
            }

            return movieList?.ToList();
        }

    }
}
