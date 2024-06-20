using FluentValidation;
using MovieApp.Infrastructure.Movies.Queries.GetMovieDetails;

namespace MovieApp.Infrastructure.Features.Movies.Queries.GetMovieDetails
{
    public sealed class GetMovieDetailsQueryValidator : AbstractValidator<GetMovieDetailsQuery>
    {
        public GetMovieDetailsQueryValidator() 
        {
            RuleFor(x => x.MovieId)
                .NotEmpty()
                .WithMessage("MovieId is required")
                .GreaterThan(0)
                .WithMessage("MovieId should be greater than zero");
        }
    }
}
