using MediatR;

namespace MovieApp.Infrastructure.Features.Movies.Commands.CreateMovie
{
    public record CreateMovieCommand(
        string? Title,
        string? Overview,
        string? Description,
        decimal Price,
        string? PictureUri,
        string? Audience,
        decimal Rating,
        DateOnly? ReleaseDate,
        int CountryId,
        int GenreId ) : IRequest<MovieCommandResponse>;    
}


