using Ardalis.GuardClauses;
using MediatR;
using MovieApp.ApplicationCore.Entities;
using MovieApp.ApplicationCore.Exceptions;
using MovieApp.ApplicationCore.Interfaces;
using MovieApp.Infrastructure.Specifications;

namespace MovieApp.Infrastructure.Features.Movies.Commands.UpdateMovie
{
    internal sealed class UpdateMovieCommandHandler :
        IRequestHandler<UpdateMovieCommand, MovieCommandResponse>
    {
        private readonly IUriComposer _uriComposer;
        private readonly IRepository<Movie> _movieRepository;

        public UpdateMovieCommandHandler(IRepository<Movie> movieRepository,
            IUriComposer uriComposer)
        {
            _uriComposer = uriComposer;
            _movieRepository = movieRepository;
        }

        public async Task<MovieCommandResponse> Handle(UpdateMovieCommand request,
            CancellationToken cancellationToken)
        {
            await MovieTitleAndPictureUriValidator(request, cancellationToken);

            var existingMovie = await _movieRepository.GetByIdAsync(request.Id, 
                cancellationToken);
            if (existingMovie is null)
            {
                throw new NotFoundException(nameof(existingMovie), request.Id.ToString());

            }

            Movie.MovieDetails details = new(request.Title, request.Overview,
                request.Description,request.Price, request.Audience, request.Rating);
                existingMovie.UpdateDetails(details);
                existingMovie.UpdateCountry(request.CountryId);
                existingMovie.UpdateGenre(request.GenreId);
                existingMovie.UpdatePictureUri(request.PictureUri!);

            await _movieRepository.UpdateAsync(existingMovie, cancellationToken);

            return new MovieCommandResponse(request.Id);
        }

        private async Task MovieTitleAndPictureUriValidator(UpdateMovieCommand request, CancellationToken cancellationToken)
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
