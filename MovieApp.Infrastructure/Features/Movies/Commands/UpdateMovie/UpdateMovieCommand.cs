using MediatR;

namespace MovieApp.Infrastructure.Features.Movies.Commands.UpdateMovie
{
    public record UpdateMovieCommand(
       int Id,
       string? Title,
       string? Overview,
       string? Description,
       decimal Price,
       string? PictureUri,
       string? Audience,
       decimal Rating,
       DateOnly? ReleaseDate,
       int CountryId,
       int GenreId) : IRequest<MovieCommandResponse>;
}
