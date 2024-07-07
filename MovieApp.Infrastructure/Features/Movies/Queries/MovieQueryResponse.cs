namespace MovieApp.Infrastructure.Features.Movies.Queries
{
    public class MovieQueryResponse
    {
        public int MovieId { get; set; }
        public string? Title { get; set; }
        public string? Overview { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? PictureUri { get; set; }
        public string? Audience { get; set; }
        public decimal Rating { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public string? CountryName { get; set; }
        public string? GenreName { get; set; }     
    };
}
