using MediatR;

namespace MovieApp.Infrastructure.Features.Genres.Queries.GetGenreList
{
    public class GetGenresListQuery : IRequest<List<GenreQueryResponse>>
    {
    }
}
