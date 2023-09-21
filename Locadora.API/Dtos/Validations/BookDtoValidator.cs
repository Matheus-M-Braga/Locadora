using System;
using FluentValidation;

namespace Locadora.API.Dtos.Validations
{
    public class BookDtoValidator : AbstractValidator<BooksDto>
    {
        public BookDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo Nome não informado.");
            RuleFor(x => x.Author)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo Autor não informado.");
            RuleFor(x => x.PublisherId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo Editora não informado.");
            RuleFor(x => x.Release)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo Lançamento não informado.");
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo Quantidade não informado.");
        }
    }
}