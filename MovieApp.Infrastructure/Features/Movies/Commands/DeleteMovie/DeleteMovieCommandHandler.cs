using Ardalis.GuardClauses;
using MediatR;
using MovieApp.Infrastructure.Entities;
using MovieApp.Infrastructure.Exceptions;
using MovieApp.Infrastructure.Interfaces;

namespace MovieApp.Infrastructure.Features.Movies.Commands.DeleteMovie
{
    internal sealed class DeleteMovieCommandHandler
        : IRequestHandler<DeleteMovieCommand>
    {
        private readonly IRepository<Movie> _movieRepository;

        public DeleteMovieCommandHandler(IRepository<Movie> movieRepository,
            IUriComposer uriComposer)
        {
            _movieRepository = movieRepository;
        }

        public async Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            if (request.Id > 0 && request.Id < 23 )
            {
                throw new DuplicateException($"Existing movie with Id: {request.Id} is not available for deletion");
            }
            var movieDelete = await _movieRepository.GetByIdAsync(request.Id);

            if (movieDelete is null)
                throw new NotFoundException(nameof(movieDelete), request.Id.ToString());

            await _movieRepository.DeleteAsync(movieDelete, cancellationToken);
        }
    }
}

  