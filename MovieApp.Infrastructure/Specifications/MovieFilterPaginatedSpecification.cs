using Ardalis.Specification;
using MovieApp.Infrastructure.Entities;

namespace MovieApp.Infrastructure.Specifications
{
    public class MovieFilterPaginatedSpecification : Specification<Movie>
    {
        public MovieFilterPaginatedSpecification(int skip, int take, int? genreId, 
            int? countryId) : base()
        {
            Query.Where(m => (!genreId.HasValue || m.GenreId == genreId) &&
           (!countryId.HasValue || m.CountryId == countryId))
                .Skip(skip).Take(take);
        }
    }
}
