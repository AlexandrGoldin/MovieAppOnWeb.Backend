using FluentValidation;

namespace MovieApp.Infrastructure.Features.Movies.Commands.DeleteMovie
{
    public sealed class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidator() 
        {
            RuleFor(x => x.Id)
               .GreaterThan(205)
               .WithMessage("The movie must be GreaterThan(205)")
               .Must(Id => Id > 205)
            //RuleFor(x => x.Id).GreaterThan(185)
               .WithMessage("The movie is not available for deletion");             
        }
    }
}


