using Ardalis.Specification;
using MovieApp.ApplicationCore.Entities;

namespace MovieApp.Infrastructure.Specifications
{
    public class MovieExistenceSpecification : Specification<Movie>
    {
        public MovieExistenceSpecification(string? movieTitle, string? moviePictureUri)
        {
            Query.Where(movie => movieTitle == movie.Title || moviePictureUri == movie.PictureUri);
        }
    }
}
