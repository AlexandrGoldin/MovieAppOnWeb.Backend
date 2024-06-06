using MediatR;

namespace MovieApp.Infrastructure.Movies.Queries.GetMovieDetails
{
    public record GetMovieDetailsQuery(int MovieId) 
        : IRequest<MovieResponse>;
      
}
