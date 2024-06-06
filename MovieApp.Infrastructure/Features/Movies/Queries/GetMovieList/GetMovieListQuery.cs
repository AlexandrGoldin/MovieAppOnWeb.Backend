using MediatR;
using MovieApp.Infrastructure.Movies.Queries.GetMovieDetails;

namespace MovieApp.Infrastructure.Movies.Queries.GetMovieList
{
    public record GetMovieListQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IRequest<PagedList<MovieResponse>>;

}
