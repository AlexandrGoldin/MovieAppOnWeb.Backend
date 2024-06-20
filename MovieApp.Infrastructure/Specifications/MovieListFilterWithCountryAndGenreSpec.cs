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
    }
}





