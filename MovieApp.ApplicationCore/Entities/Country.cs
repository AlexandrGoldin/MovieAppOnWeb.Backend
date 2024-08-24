using MovieApp.ApplicationCore.Interfaces;

namespace MovieApp.ApplicationCore.Entities
{
    public class Country : BaseEntity, IAggregateRoot
    {
        public string CountryName { get; init; }

        public Country(string countryName)
        {
            CountryName = countryName;
        }

        public static explicit operator string (Country country) 
            => country.CountryName;

        public List<Movie> Movies { get; private set; } = new List<Movie>();
       

    }
}
