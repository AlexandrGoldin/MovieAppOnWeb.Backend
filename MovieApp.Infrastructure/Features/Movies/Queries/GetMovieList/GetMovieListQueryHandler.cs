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

            var spec = new MovieListFilterWithCountryAndGenreSpec(request.SearchTerm);

            var movieList = (await _movieRepository.ListAsync(spec,
                cancellationToken));

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
                .Select(m => new MovieQueryResponse {
                    MovieId = m.Id,
                    Title = m.Title!,
                    Overview = m.Overview!,
                    Description = m.Description!,
                    Price =  m.Price,
                    PictureUri = _uriComposer.ComposePicUri(m.PictureUri!),
                    Audience = m.Audience!,
                    Rating = m.Rating,
                    ReleaseDate = m.ReleaseDate,
                    CountryName = m.Country!.CountryName,
                    GenreName = m.Genre!.GenreName});
           
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
    }
}
