using Ardalis.GuardClauses;
using MediatR;
using MovieApp.Infrastructure.Entities;
using MovieApp.Infrastructure.Interfaces;
using MovieApp.Infrastructure.Specifications;

namespace MovieApp.Infrastructure.Movies.Queries.GetMovieDetails
{
    public class GetMovieDetailsQueryHandlef 
        : IRequestHandler<GetMovieDetailsQuery, MovieResponse>
    {
        private readonly IUriComposer _uriComposer;
        private readonly IReadRepository<Movie> _movieRepository;

        public GetMovieDetailsQueryHandlef(IReadRepository<Movie> movieRepository,
            IUriComposer uriComposer) 
        {
            _uriComposer = uriComposer;
            _movieRepository = movieRepository;
        }

        public async Task<MovieResponse> Handle(GetMovieDetailsQuery request, 
            CancellationToken cancellationToken)
        {
            if(request.MovieId == 0)
                throw new NotFoundException(nameof(request), request.MovieId.ToString());

            var spec = new MovieDetailsWithCountryAndGenreSpec(request.MovieId);
            var movie = await _movieRepository.FirstOrDefaultAsync(spec,
                cancellationToken);
            if (movie is null)    
                throw new NotFoundException(nameof(movie), request.MovieId.ToString());

            return new MovieResponse
            {
                Id = request.MovieId,
                Title = movie.Title,
                Overview = movie.Overview,
                Description = movie.Description,
                PictureUri = _uriComposer.ComposePicUri(movie.PictureUri!),
                Audience = movie.Audience,
                Rating = movie.Rating,
                ReleaseDate = movie.ReleaseDate,
                CountryName = movie.Country!.CountryName,
                GenreName = movie.Genre!.GenreName
            };
        }
    }
}
