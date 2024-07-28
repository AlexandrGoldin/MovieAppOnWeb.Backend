using Ardalis.Specification;
using MovieApp.Infrastructure.Entities;

namespace MovieApp.Infrastructure.Specifications
{
    public class MovieListFilterWithCountryAndGenreSpec : Specification<Movie>
    {

        public MovieListFilterWithCountryAndGenreSpec(string? searchTerm)
        {
            Query
                .Include(c => c.Country)
                .Include(g => g.Genre);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                Query
                     .Where(m =>
                     m.ReleaseDate!.ToString()!.Contains(searchTerm!) ||
                     m.Title!.ToLower().Contains(searchTerm.ToLower()) ||
                     m.Country!.CountryName!.ToLower().Contains(searchTerm!.ToLower()) ||
                     m.Genre!.GenreName!.ToLower().Contains(searchTerm.ToLower()));
            }
        }

        public MovieListFilterWithCountryAndGenreSpec(int minDate, int maxDate)
        {
            Query
                .Include(c => c.Country)
                .Include(g => g.Genre)
                .Where(m => m.ReleaseDate >= new DateOnly(minDate, 1, 1) && m.ReleaseDate <= new DateOnly(maxDate, 12, 31));
        }
    }
}





