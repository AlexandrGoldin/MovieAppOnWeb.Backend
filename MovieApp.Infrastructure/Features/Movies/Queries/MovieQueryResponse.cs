using Ardalis.GuardClauses;

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

#pragma warning disable CS8765
        public override bool Equals(object obj)
        {
            Guard.Against.NullOrEmpty(Title, nameof(Title));
            Guard.Against.NullOrEmpty(Overview, nameof(Overview));
            Guard.Against.NullOrEmpty(Description, nameof(Description));
            Guard.Against.NegativeOrZero(Price, nameof(Price));
            Guard.Against.NullOrEmpty(PictureUri, nameof(PictureUri));
            Guard.Against.NullOrEmpty(Audience, nameof(Audience));
            Guard.Against.NegativeOrZero(Rating, nameof(Rating));
            Guard.Against.NullOrEmpty(ReleaseDate.ToString(), nameof(ReleaseDate));
            Guard.Against.NullOrEmpty(CountryName, nameof(CountryName));
            Guard.Against.NullOrEmpty(GenreName, nameof(GenreName));

            return this.MovieId == ((MovieQueryResponse)obj).MovieId 
                && this.Title == ((MovieQueryResponse)obj).Title
                && this.Overview == ((MovieQueryResponse)obj).Overview
                && this.Description == ((MovieQueryResponse)obj).Description
                && this.Price == ((MovieQueryResponse)obj).Price
                && this.PictureUri == ((MovieQueryResponse)obj).PictureUri
                && this.Audience == ((MovieQueryResponse)obj).Audience
                && this.Rating == ((MovieQueryResponse)obj).Rating
                && this.ReleaseDate == ((MovieQueryResponse)obj).ReleaseDate
                && this.CountryName == ((MovieQueryResponse)obj).CountryName
                && this.GenreName == ((MovieQueryResponse)obj).GenreName;
        }

        public override int GetHashCode()
        {
            int MovieIdHashCode = this.MovieId.GetHashCode();
            int TitleHashCode = this.Title == null ? 0 : this.Title.GetHashCode();
            int OverviewHashCode = this.Overview == null ? 0 : this.Overview.GetHashCode();
            int DescriptionHashCode = this.Description == null ? 0 : this.Description.GetHashCode();
            int PriceHashCode = this.Price.GetHashCode();
            int PictureUriHashCode = this.PictureUri == null ? 0 : this.PictureUri.GetHashCode();
            int AudienceHashCode = this.Audience == null ? 0 : this.Audience.GetHashCode();
            int RatingHashCode = this.Rating.GetHashCode();
            int ReleaseDateHashCode = this.ReleaseDate == null ? 0 : this.ReleaseDate.GetHashCode();
            int CountryNameHashCode = this.CountryName == null ? 0 : this.CountryName.GetHashCode();
            int GenreNameHashCode = this.GenreName == null ? 0 : this.GenreName.GetHashCode();
           
            return MovieIdHashCode 
                ^ TitleHashCode 
                ^ OverviewHashCode 
                ^ DescriptionHashCode
                ^ PriceHashCode 
                ^ PictureUriHashCode 
                ^ AudienceHashCode 
                ^ RatingHashCode
                ^ ReleaseDateHashCode 
                ^ CountryNameHashCode 
                ^ GenreNameHashCode;
        }
    };
}
