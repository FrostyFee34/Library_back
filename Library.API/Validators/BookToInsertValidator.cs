using FluentValidation;
using Library.API.DTOs;

namespace Library.API.Validators;

public class BookToInsertValidator : AbstractValidator<BookToInsertDTO>
{
    public BookToInsertValidator()
    {
        RuleFor(book=>book.Title).NotEmpty().WithErrorCode("400").WithMessage("Book must have title");
        RuleFor(book=>book.Genre).NotEmpty().WithErrorCode("400").WithMessage("Book must have genre");
        RuleFor(book=>book.Author).NotEmpty().WithErrorCode("400").WithMessage("Book must have author");
    }
}