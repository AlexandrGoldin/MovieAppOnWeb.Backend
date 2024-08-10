using MediatR;
using MovieApp.Infrastructure.Features.Movies.Queries;

namespace MovieApp.Infrastructure.Movies.Queries.GetMovieList
{
    public record GetMovieListQuery(MovieQueryParams QueryParams) 
        : IRequest<PagedList<MovieQueryResponse>>;
}
