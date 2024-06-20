using FluentValidation;

namespace MovieApp.Infrastructure.Features.Movies.Commands.CreateMovie
{
    public sealed class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .Length(5, 50).WithMessage("Title length from 5 to 50 char.");
            RuleFor(x => x.Overview)
                .NotEmpty().WithMessage("Overview is required.")
                .Length(20, 200).WithMessage("Overview length from 20 to 200 char.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .Length(20, 300).WithMessage("Description length from 20 to 300 char.");
            RuleFor(x => x.Audience)
                .NotEmpty().WithMessage("Audience is required.")
                .MaximumLength(3).WithMessage("Audience length is 3 char.");
            RuleFor(x => x.Rating)
                .GreaterThan(0).WithMessage("---Rating Greater Than zero.")
                .LessThanOrEqualTo(10).WithMessage("---Rating Less Than Or Equal 10");
            RuleFor(x => x.ReleaseDate)
                .Must(date => date != default(DateOnly))
                .NotEmpty().WithMessage("ReleaseDate property must be of type DateOnly.");         
            RuleFor(x => x.CountryId)
                .NotEmpty().WithMessage("CountryId is required.")
                .GreaterThan(0).WithMessage("CountryId Greater Than zero.");
            RuleFor(x => x.GenreId)
                .NotEmpty().WithMessage("GenreId is required.")
                .GreaterThan(0).WithMessage("GenreId Greater Than zero.");
        }
    }
}
