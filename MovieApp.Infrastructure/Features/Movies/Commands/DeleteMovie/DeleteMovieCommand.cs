using MediatR;

namespace MovieApp.Infrastructure.Features.Movies.Commands.DeleteMovie
{
    public record DeleteMovieCommand(int Id) : IRequest;

}
