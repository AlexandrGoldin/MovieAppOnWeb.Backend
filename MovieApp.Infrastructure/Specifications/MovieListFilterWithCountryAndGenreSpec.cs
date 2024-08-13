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


            ApplySearchTermFilter(queryParams.SearchTerm);
            ApplyGenreFilter(queryParams.WithGenres);
            ApplyCountryFilter(queryParams.WithCountries);
            ApplyReleaseYearFilter(queryParams.PrimaryReleaseYear);
        }
        private void ApplySearchTermFilter(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                Query.Where(m =>
                    (m.Title != null && m.Title.ToLower().Contains(searchTerm)) ||
                    (m.Genre != null && m.Genre.GenreName.ToLower().Contains(searchTerm)) ||
                    (m.Country != null && m.Country.CountryName.ToLower().Contains(searchTerm)));
            }
        }

        private void ApplyGenreFilter(string? withGenres)
        {
            if (!string.IsNullOrWhiteSpace(withGenres))
            {
                var genreIds = withGenres.Split(',').Select(int.Parse).ToArray();
                Query.Where(m => genreIds.Contains(m.GenreId));
            }
        }

        private void ApplyCountryFilter(string? withCountries)
        {
            if (!string.IsNullOrWhiteSpace(withCountries))
            {
                var countryIds = withCountries.Split(',').Select(int.Parse).ToArray();
                Query.Where(m => countryIds.Contains(m.CountryId));
            }
        }

        private void ApplyReleaseYearFilter(string? primaryReleaseYear)
        {
            if (!string.IsNullOrWhiteSpace(primaryReleaseYear))
            {
                var yearParts = primaryReleaseYear.Split('.');
                if (yearParts.Length == 1 && int.TryParse(yearParts[0], out var year))
                {
                    Query.Where(m => m.ReleaseDate.HasValue && m.ReleaseDate.Value.Year == year);
                }
                else if (yearParts.Length == 2 && int.TryParse(yearParts[0], out var startYear) && int.TryParse(yearParts[1], out var endYear))
                {
                    Query.Where(m => m.ReleaseDate.HasValue && m.ReleaseDate.Value.Year >= startYear && m.ReleaseDate.Value.Year <= endYear);
                }
            }
        }
    }
}







