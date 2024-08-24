using MovieApp.ApplicationCore.Interfaces;

namespace MovieApp.ApplicationCore.Entities
{
    public class Genre : BaseEntity, IAggregateRoot
    {
        public string GenreName { get; init; }

        public Genre(string genreName)
        {
            GenreName = genreName;
        }

        public static explicit operator string(Genre genre)
            => genre.GenreName;

        public List<Movie> Movies { get; init; } = new List<Movie>();


    }
}



