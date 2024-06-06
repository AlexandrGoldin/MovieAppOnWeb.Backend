namespace MovieApp.Application.Entities
{
    public class Genre : BaseEntity
    {
        public string GenreName { get; private set; }
        public List<Movie> Movies { get; private set; } = new List<Movie>();
        public Genre(string genreName) 
        { 
            GenreName = genreName;
        }
    }

    //public enum GenreType {
    //    Комедия, Боевик, Мелодрама, Драма, Детектив, Триллер 
    //}
}
