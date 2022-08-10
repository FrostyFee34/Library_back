using FluentValidation;
using Library.API.DTOs;

namespace Library.API.Validators;

public class ReviewToInsertValidator : AbstractValidator<ReviewToInsertDTO>
{
    public ReviewToInsertValidator()
    {
        RuleFor(review => review.Reviewer).NotEmpty().WithErrorCode("400").WithMessage("Reviewer name must not be empty");
        RuleFor(review => review.Message).NotEmpty().WithErrorCode("400").WithMessage("Message must not be empty");
    }
}