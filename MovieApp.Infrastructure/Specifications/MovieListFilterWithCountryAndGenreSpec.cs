using Ardalis.Specification;
using MovieApp.Infrastructure.Entities;
using MovieApp.Infrastructure.Features.Movies.Queries;

namespace MovieApp.Infrastructure.Specifications
{

    public class MovieListFilterWithCountryAndGenreSpec : Specification<Movie>
    {
        public MovieListFilterWithCountryAndGenreSpec(MovieQueryParams queryParams)
        {
            Query
                .Include(c => c.Country)
                .Include(g => g.Genre);

            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                var searchTerm = queryParams.SearchTerm.ToLower();
                Query.Where(m =>
                    (m.Title != null && m.Title.ToLower().Contains(searchTerm)) ||
                    (m.Genre != null && m.Genre.GenreName != null && m.Genre.GenreName.ToLower().Contains(searchTerm)) ||
                    (m.Country != null && m.Country.CountryName != null && m.Country.CountryName.ToLower().Contains(searchTerm)));
            }

            if (!string.IsNullOrWhiteSpace(queryParams.WithGenres))
            {
                string[] stringArrayGenre = queryParams.WithGenres.Split(',');
                var arrayGenreIds = Array.ConvertAll(stringArrayGenre, int.Parse);

                Query.Where(m => arrayGenreIds.Contains(m.GenreId));
            }

            if(string.IsNullOrWhiteSpace(queryParams.WithGenres) 
                && !string.IsNullOrWhiteSpace(queryParams.WithCountries))
            {
                string[] stringArrayCountry = queryParams.WithCountries.Split(',');
                var arrayCountryIds = Array.ConvertAll(stringArrayCountry, int.Parse);

                Query.Where(m => arrayCountryIds.Contains(m.CountryId));
            }

            if (!string.IsNullOrWhiteSpace(queryParams.PrimaryReleaseYear))
            {
                var yearParts = queryParams.PrimaryReleaseYear.Split('.');
                if (yearParts.Length == 1 && int.TryParse(yearParts[0], out var year))
                {
                    // One year
                    Query.Where(m => m.ReleaseDate.HasValue && m.ReleaseDate.Value.Year == year);
                }
                else if (yearParts.Length == 2 && int.TryParse(yearParts[0], out var startYear) && int.TryParse(yearParts[1], out var endYear))
                {
                    // Range of years
                    Query.Where(m => m.ReleaseDate.HasValue && m.ReleaseDate.Value.Year >= startYear && m.ReleaseDate.Value.Year <= endYear);
                }
            }
        }
    }
}







