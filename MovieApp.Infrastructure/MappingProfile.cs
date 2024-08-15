using AutoMapper;
using MovieApp.Infrastructure.Entities;
using MovieApp.Infrastructure.Features.Countries.Queries;
using MovieApp.Infrastructure.Features.Genres.Queries;
using MovieApp.Infrastructure.Features.Movies.Queries;

namespace MovieApp.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // The value of property MovieQueryResponse.PictureUri is set in the
            // method MovieFilteringService.MappingMovieListToMovieQueryResponseList.
            CreateMap<Movie, MovieQueryResponse>()
                .ForMember(movieResponse => movieResponse.MovieId,
                movie => movie.MapFrom(movie => movie.Id))
                 .ForMember(movieResponse => movieResponse.Title,
                movie => movie.MapFrom(movie => movie.Title))
                 .ForMember(movieResponse => movieResponse.Overview,
                movie => movie.MapFrom(movie => movie.Overview))
                 .ForMember(movieResponse => movieResponse.Description,
                movie => movie.MapFrom(movie => movie.Description))
                  .ForMember(movieResponse => movieResponse.Price,
                movie => movie.MapFrom(movie => movie.Price))
                 .ForMember(movieResponse => movieResponse.Audience,
                movie => movie.MapFrom(movie => movie.Audience))
                 .ForMember(movieResponse => movieResponse.Rating,
                movie => movie.MapFrom(movie => movie.Rating))
                  .ForMember(movieResponse => movieResponse.ReleaseDate,
                movie => movie.MapFrom(movie => movie.ReleaseDate))
                  .ForMember(movieResponse => movieResponse.CountryName,
                movie => movie.MapFrom(movie => movie.Country!.CountryName))
                  .ForMember(movieResponse => movieResponse.GenreName,
                movie => movie.MapFrom(movie => movie.Genre!.GenreName)).ReverseMap();

            CreateMap<Genre, GenreQueryResponse>().ReverseMap();

            CreateMap<Country, CountryQueryResponse>().ReverseMap();

        }
    }
}
