using Ardalis.GuardClauses;
using MediatR;
using MovieApp.ApplicationCore.Entities;
using MovieApp.ApplicationCore.Exceptions;
using MovieApp.ApplicationCore.Interfaces;
using MovieApp.Infrastructure.Specifications;

namespace MovieApp.Infrastructure.Features.Movies.Commands.CreateMovie
{
    internal sealed class CreateMovieCommandHandler : 
        IRequestHandler<CreateMovieCommand, MovieCommandResponse>
    {
        private readonly IUriComposer _uriComposer;
        private readonly IRepository<Movie> _movieRepository;

        public CreateMovieCommandHandler(IRepository<Movie> movieRepository,
            IUriComposer uriComposer)
        {
            _uriComposer = uriComposer;
            _movieRepository = movieRepository;
        }

        public async Task<MovieCommandResponse> Handle(CreateMovieCommand request,
            CancellationToken cancellationToken)
        {
            await MovieTitleAndPictureUriValidator(request, cancellationToken);

            var newMovie = new Movie(
            request.Title,
            request.Overview,
            request.Description,
            request.Price,
            _uriComposer.ComposePicUri(request.PictureUri!),
            request.Audience,
            request.Rating,
            request.ReleaseDate,
            request.CountryId,
            request.GenreId);

            newMovie = await _movieRepository.AddAsync(newMovie, cancellationToken);

            return new MovieCommandResponse(newMovie.Id);
        }

        private async Task MovieTitleAndPictureUriValidator(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var movieExistenceSpecification = new MovieExistenceSpecification(request.Title, request.PictureUri);
            var existingMovies = await _movieRepository.ListAsync(movieExistenceSpecification, cancellationToken);

            if (existingMovies.Any(movie => movie.Title == request.Title))
            {
                throw new DuplicateException($"A movie with name {request.Title} already exists");
            }

            if (!existingMovies.Any(movie => movie.PictureUri == request.PictureUri))
            {
                throw new NotFoundException(nameof(request.PictureUri), request.Title!.ToString());
            }
        }
    }
}
