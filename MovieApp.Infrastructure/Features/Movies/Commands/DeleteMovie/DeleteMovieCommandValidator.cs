using FluentValidation;

namespace MovieApp.Infrastructure.Features.Movies.Commands.DeleteMovie
{
    public sealed class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidator() 
        {
            RuleFor(x => x.Id)
               .GreaterThan(0)
               .WithMessage("The movie.Id must be GreaterThan(0)");
           
        }
    }
}


