using Ardalis.Specification;
using MovieApp.Infrastructure.Entities;

namespace MovieApp.Infrastructure.Specifications
{
    public class MovieAudienceSpecification : Specification<Movie>
    {
        public MovieAudienceSpecification(string movieAudience) 
        {
            Query.Where(movie => movieAudience == movie.Audience);
        }
    }
}
