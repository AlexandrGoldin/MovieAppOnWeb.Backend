using Ardalis.Specification;
using MovieApp.Infrastructure.Entities;

namespace MovieApp.Infrastructure.Specifications
{
    public class MovieDetailsWithCountryAndGenreSpec : Specification<Movie>
    {
        public MovieDetailsWithCountryAndGenreSpec(int movieId)
        {
            Query
                .Where(movie => movie.Id == movieId)
                .Include(c => c.Country)
                .Include(g => g.Genre);
        }
    }
}
