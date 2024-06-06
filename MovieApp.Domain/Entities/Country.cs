namespace MovieApp.Application.Entities
{
    public class Country : BaseEntity
    {
        public string CountryName { get; private set; }

        public List<Movie> Movies { get; private set; } = new List<Movie>();
        public Country(string countryName) 
        {
            CountryName = countryName;
        }

    }
}
