using Ardalis.Specification;
using MovieApp.Infrastructure.Entities;

namespace MovieApp.Infrastructure.Specifications
{
    public class MovieFilterSpecification : Specification<Movie>
    {
        public MovieFilterSpecification(int? genreId, int? countryId) 
        {
            Query.Where(m => (!genreId.HasValue || m.GenreId == genreId)&&
            (!countryId.HasValue || m.CountryId == countryId));
        }
    }
}
