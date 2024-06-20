using Ardalis.GuardClauses;
using MediatR;
using MovieApp.Infrastructure.Common.Exceptions;
using MovieApp.Infrastructure.Entities;
using MovieApp.Infrastructure.Features.Movies.Queries;
using MovieApp.Infrastructure.Interfaces;
using MovieApp.Infrastructure.Specifications;

namespace MovieApp.Infrastructure.Movies.Queries.GetMovieDetails
{
    public class GetMovieDetailsQueryHandler 
        : IRequestHandler<GetMovieDetailsQuery, GetMovieGetByIdResponse>
    {
        private readonly IUriComposer _uriComposer;
        private readonly IReadRepository<Movie> _movieRepository;

        public GetMovieDetailsQueryHandler(IReadRepository<Movie> movieRepository,
            IUriComposer uriComposer) 
        {
            _uriComposer = uriComposer;
            _movieRepository = movieRepository;
        }

        public async Task<GetMovieGetByIdResponse> Handle(GetMovieDetailsQuery request,
            CancellationToken cancellationToken)
        {           
            var spec = new MovieDetailsWithCountryAndGenreSpec(request.MovieId);
            var movie = await _movieRepository.FirstOrDefaultAsync(spec,
                cancellationToken);

            if (movie is null)
                throw new NotFoundException(nameof(movie), request.MovieId.ToString());

            return new GetMovieGetByIdResponse(
                request.MovieId,
                movie.Title!,
                movie.Overview!,
                movie.Description!,
                _uriComposer.ComposePicUri(movie.PictureUri!),
                movie.Audience!,
                movie.Rating,
                movie.ReleaseDate,
                movie.Country!.CountryName,
                movie.Genre!.GenreName
                );
        }
    }
}
