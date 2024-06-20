using Ardalis.GuardClauses;
using Azure.Core;
using MediatR;
using MovieApp.Infrastructure.Entities;
using MovieApp.Infrastructure.Features.Movies.Commands;
using MovieApp.Infrastructure.Interfaces;

namespace MovieApp.Infrastructure.Features.Movies.Commands.DeleteMovie
{
    internal sealed class DeleteMovieCommandHandler
        : IRequestHandler<DeleteMovieCommand, MovieCommandResponse>
    {
        private readonly IRepository<Movie> _movieRepository;

        public DeleteMovieCommandHandler(IRepository<Movie> movieRepository,
            IUriComposer uriComposer)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieCommandResponse> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var movieDelete = await _movieRepository.GetByIdAsync(request.Id);

            ////return StatusCode 500???. Need replace StatusCode on 404!!!
            if (movieDelete is null)
                throw new NotFoundException(nameof(movieDelete), request.Id.ToString());

            await _movieRepository.DeleteAsync(movieDelete, cancellationToken);

            return new MovieCommandResponse(request.Id);
        }
    }
}

// return new MovieCommandResponse(request.Id);
