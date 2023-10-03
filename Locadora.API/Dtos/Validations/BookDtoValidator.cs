using System;
using FluentValidation;

namespace Locadora.API.Dtos.Validations
{
    public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookDtoValidator()
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

    public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookDtoValidator()
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