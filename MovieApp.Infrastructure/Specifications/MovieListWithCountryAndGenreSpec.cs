using Ardalis.Specification;
using MovieApp.Infrastructure.Entities;

namespace MovieApp.Infrastructure.Specifications
{
    public class MovieListWithCountryAndGenreSpec : Specification<Movie>
    {
        public MovieListWithCountryAndGenreSpec() 
        {
            Query
               .Include(c => c.Country)
               .Include(g => g.Genre);
        }
    }
}
