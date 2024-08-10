namespace MovieApp.Infrastructure.Features.Movies.Queries
{
    public class MovieQueryParams
    {
        public string? SearchTerm {  get; set; }
        public string? PrimaryReleaseYear { get; set; } // PrimaryReleaseYear in format "2010.2020" or "2020"
        public string? WithGenres { get; set; }
        public string? WithCountries { get; set; }
        public string? SortBy { get; set; } // sortBy in format "field.order"
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
