using MediatR;
using MovieApp.Infrastructure.Features.Movies.Queries;
using MovieApp.Infrastructure.Interfaces;

namespace MovieApp.Infrastructure.Movies.Queries.GetMovieList
{
    internal sealed class GetMovieListQueryHandler :
        IRequestHandler<GetMovieListQuery, PagedList<MovieQueryResponse>>
    {
        private readonly IUriComposer _uriComposer;
        private readonly IMovieFilteringService _movieFilteringService;
        private readonly IMovieSortingService _movieSortingService;

        public GetMovieListQueryHandler(IMovieFilteringService movieFilteringService, 
            IMovieSortingService movieSortingService, IUriComposer uriComposer)
        {
            _uriComposer = uriComposer;
            _movieFilteringService = movieFilteringService;
            _movieSortingService = movieSortingService;
        }

        public async Task<PagedList<MovieQueryResponse>> Handle(GetMovieListQuery request, 
            CancellationToken cancellationToken)
        {
            await Task.Delay(1000);

            var queryParams = request.QueryParams;

            var movieList = await _movieFilteringService.FilteringMoviesAsync(request.QueryParams,
            cancellationToken);
          
            movieList = await _movieSortingService.SortingMoviesAsync(request.QueryParams, movieList);

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
    }
}


