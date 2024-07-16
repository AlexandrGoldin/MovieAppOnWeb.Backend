using Ardalis.GuardClauses;
using MediatR;
using MovieApp.Infrastructure.Entities;
using MovieApp.Infrastructure.Features.Movies.Queries;
using MovieApp.Infrastructure.Interfaces;
using MovieApp.Infrastructure.Specifications;

namespace MovieApp.Infrastructure.Movies.Queries.GetMovieDetails
{
    public class GetMovieDetailsQueryHandler 
        : IRequestHandler<GetMovieDetailsQuery, MovieQueryResponse>
    {
        private readonly IUriComposer _uriComposer;
        private readonly IReadRepository<Movie> _movieRepository;

        public GetMovieDetailsQueryHandler(IReadRepository<Movie> movieRepository,
            IUriComposer uriComposer) 
        {
            _uriComposer = uriComposer;
            _movieRepository = movieRepository;
        }

        public async Task<MovieQueryResponse> Handle(GetMovieDetailsQuery request,
            CancellationToken cancellationToken)
        {           
            var spec = new MovieDetailsWithCountryAndGenreSpec(request.MovieId);
            var movie = await _movieRepository.FirstOrDefaultAsync(spec,
                cancellationToken);

            if (movie is null)
                throw new NotFoundException(nameof(movie), request.MovieId.ToString());

            return new MovieQueryResponse {
                MovieId = request.MovieId,
                Title = movie.Title!,
                Overview = movie.Overview!,
                Description = movie.Description!,
                Price = movie.Price,
                PictureUri = _uriComposer.ComposePicUri(movie.PictureUri!),
                Audience = movie.Audience!,
                Rating = movie.Rating,
                ReleaseDate = movie.ReleaseDate,
                CountryName = movie.Country!.CountryName,
                GenreName = movie.Genre!.GenreName
                };
        }
    }
}
