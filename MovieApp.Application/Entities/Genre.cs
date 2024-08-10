using MovieApp.Infrastructure.Interfaces;

namespace MovieApp.Infrastructure.Entities
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


    //public enum GenreType {
    //    Комедия, Боевик, Мелодрама, Драма, Детектив, Триллер 
    //}

