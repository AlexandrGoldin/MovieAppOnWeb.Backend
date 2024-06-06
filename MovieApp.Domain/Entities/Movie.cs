using Ardalis.GuardClauses;

namespace MovieApp.Application.Entities
{
    public class Movie : BaseEntity, IAggregateRoot
    {
        public string? Title { get; private set; }
        public string? Overview { get; private set; }
        public string? Description { get; private set; }
        public string? PictureUri { get; private set; }
        public string? Audience { get; private set; }
        public decimal Rating { get; private set; }
        public DateTimeOffset? ReleaseDate { get; private set; }
        public int CountryId { get; private set; }
        public Country? Country { get; private set; }
        public int GenreId { get; private set; }
        public Genre? Genre { get; private set; }

        public Movie() { }

        public Movie(string? title, string? overview, string? description, 
            string? pictureUri, string? audience, decimal rating,
            DateTimeOffset? releaseDate, int countryId, int genreId)
        {
            Title = title;
            Overview = overview;
            Description = description;
            PictureUri = pictureUri;
            Audience = audience;
            Rating = rating;
            ReleaseDate = releaseDate;
            CountryId = countryId;
            GenreId = genreId;
        }

        public void UpdateDetails(MovieDetails details)
        {
            Guard.Against.NullOrEmpty(details.Title, nameof(details.Title));
            Guard.Against.NullOrEmpty(details.Overview, nameof(details.Overview));
            Guard.Against.NullOrEmpty(details.Description, nameof(details.Description));
            Guard.Against.NegativeOrZero(details.Rating, nameof(details.Rating));

            Title = details.Title;
            Overview = details.Overview;
            Description = details.Description;
            Rating= details.Rating;
        }

        public void UpdateCountry(int countryId)
        {
            Guard.Against.Zero(countryId, nameof(countryId));
            CountryId = countryId;
        }

        public void UpdateGenre(int genreId)
        {
            Guard.Against.Zero(genreId, nameof(genreId));
            GenreId = genreId;
        }

        public void UpdateReleaseDate(DateTimeOffset releaseDate)
        {
            string releaseDateToString = String.Format("{yyyy}",releaseDate);
            if(string.IsNullOrEmpty(releaseDateToString))
            {
                return;
            }
           
            ReleaseDate = releaseDate;
        }

        public void UpdatePictureUri(string pictureName)
        {
            if(string.IsNullOrEmpty(pictureName))
            {
                PictureUri = string.Empty;
                return;
            }
            PictureUri = $"images\\products\\{pictureName}";
        }

        public readonly record struct MovieDetails
        {
            public string? Title {get;}
            public string? Overview {get;}
            public string? Description { get; }
            public decimal Rating { get;}
        }          
    }
} 

