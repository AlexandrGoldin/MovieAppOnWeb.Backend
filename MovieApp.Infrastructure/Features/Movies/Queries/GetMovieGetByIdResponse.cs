namespace MovieApp.Infrastructure.Features.Movies.Queries
{
    public class GetMovieGetByIdResponse
    {
        public int Id { get; init; }
        public string? Title { get; init; }
        public string? Overview { get; init; }
        public string? Description { get; init; }
        public string? PictureUri { get; init; }
        public string? Audience { get; init; }
        public decimal Rating { get; init; }
        public DateOnly? ReleaseDate { get; init; }
        public string? CountryName { get; init; }
        public string? GenreName { get; init; }

        public GetMovieGetByIdResponse
            (int id, 
            string title, 
            string overview,
            string description,
            string pictureUri,
            string audience,
            decimal rating,
            DateOnly? releaseDate,
            string? countryName,
            string? genreName ) 
        { 
            Id = id;
            Title = title;
            Overview = overview;
            Description = description;
            PictureUri = pictureUri;
            Audience = audience;
            Rating = rating;
            ReleaseDate = releaseDate;
            CountryName = countryName;
            GenreName = genreName;
        }
    };
}
