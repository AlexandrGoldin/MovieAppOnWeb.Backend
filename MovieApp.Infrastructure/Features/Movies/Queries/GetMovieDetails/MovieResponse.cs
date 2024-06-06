namespace MovieApp.Infrastructure.Movies.Queries.GetMovieDetails
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Overview{ get; set; }
        public string? Description{ get; set; }
        public string? PictureUri{ get; set; }
        public string? Audience{ get; set; }
        public decimal Rating { get; set; }
        public DateOnly? ReleaseDate{ get; set; }
        public string? CountryName{ get; set; }
        public string? GenreName { get; set; }
    };
}
