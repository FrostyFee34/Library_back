using FluentValidation;
using Library.API.DTOs;

namespace Library.API.Validators;

public class RatingToInsertValidator : AbstractValidator<RatingToInsertDTO>
{
    public RatingToInsertValidator()
    {
        RuleFor(rating => rating.Score).InclusiveBetween(1, 5).WithErrorCode("400")
            .WithMessage("Score must be between 1 an 5");
    }
}