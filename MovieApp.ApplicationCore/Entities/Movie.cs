using Ardalis.GuardClauses;
using MovieApp.ApplicationCore.Interfaces;

namespace MovieApp.ApplicationCore.Entities
{
    public class Movie : BaseEntity, IAggregateRoot
    {
        public string? Title { get; private set; }
        public string? Overview { get; private set; }
        public string? Description { get; private set; }
        public decimal Price { get; private set; }
        public string? PictureUri { get; private set; }
        public string? Audience { get; private set; }
        public decimal Rating { get; private set; }
        public DateOnly? ReleaseDate { get; private set; }
        public int CountryId { get; private set; }
        public Country? Country { get; private set; }
        public int GenreId { get; private set; }
        public Genre? Genre { get; private set; }

        public Movie() { }

        public Movie(string? title, string? overview, string? description,
            decimal price, string? pictureUri, string? audience, decimal rating,
            DateOnly? releaseDate, int countryId, int genreId)
        {
            Title = title;
            Overview = overview;
            Description = description;
            Price = price;
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
            Guard.Against.NegativeOrZero(details.Price, nameof(details.Price));
            Guard.Against.NullOrEmpty(details.Audience, nameof(details.Audience));
            Guard.Against.NegativeOrZero(details.Rating, nameof(details.Rating));

            Title = details.Title;
            Overview = details.Overview;
            Description = details.Description;
            Price = details.Price;
            Audience = details.Audience;
            Rating = details.Rating;
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

        public void UpdateReleaseDate(DateOnly? releaseDate)
        {
            string releaseDateToString = String.Format("{yyyy}",releaseDate);
            if(string.IsNullOrEmpty(releaseDateToString))
            {
                return;
            }
           
            ReleaseDate = releaseDate;
        }

        public void UpdatePictureUri(string pictureUri)
        {
            if(string.IsNullOrEmpty(pictureUri))
            {
                PictureUri = string.Empty;
                return;
            }
            PictureUri = pictureUri;
        }

        public readonly record struct MovieDetails
        {
            public string? Title {get;}
            public string? Overview {get;}
            public string? Description { get; }
            public decimal Price { get; }

            public string? Audience { get; }
            public decimal Rating { get;}

            public MovieDetails(string? title, string? overview, 
                string? description, decimal price,  string? audience, decimal rating)
            {
                Title = title;
                Overview = overview;
                Description = description;
                Price = price;
                Audience = audience;
                Rating = rating;
            }
        }          
    }
} 

