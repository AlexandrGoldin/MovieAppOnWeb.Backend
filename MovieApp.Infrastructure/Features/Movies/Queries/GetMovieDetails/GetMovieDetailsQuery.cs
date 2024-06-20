using MediatR;
using MovieApp.Infrastructure.Features.Movies.Queries;

namespace MovieApp.Infrastructure.Movies.Queries.GetMovieDetails
{
    public record GetMovieDetailsQuery(int MovieId) 
        : IRequest<GetMovieGetByIdResponse>;
      
}
