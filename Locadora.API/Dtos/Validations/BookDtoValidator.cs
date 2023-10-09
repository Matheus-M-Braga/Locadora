using System;
using FluentValidation;

namespace Locadora.API.Dtos.Validations
{
    public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Campo Nome não informado.")
                .MinimumLength(3).WithMessage("Necessário pelo menos 3 caracteres.")
                .MaximumLength(50).WithMessage("Limite é de 50 caracteres.");
            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Campo Autor não informado.")
                .MinimumLength(3).WithMessage("Necessário pelo menos 3 caracteres.")
                .MaximumLength(50).WithMessage("Limite é de 50 caracteres.");
            RuleFor(x => x.PublisherId)
                .NotEmpty().WithMessage("Campo EditoraId não informado.")
                .GreaterThanOrEqualTo(1).WithMessage("Campo EditoraId não informado");
            RuleFor(x => x.Release)
                .NotEmpty().WithMessage("Campo Lançamento não informado.")
                .MinimumLength(3).WithMessage("Necessário pelo menos 3 caracteres.")
                .MaximumLength(50).WithMessage("Limite é de 50 caracteres.");
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Campo Quantidade não informado.")
                .GreaterThanOrEqualTo(1).WithMessage("Campo Quantidade não informado.");
        }
    }

    public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookDtoValidator()
        {
             RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Campo Id não informado.")
                .GreaterThanOrEqualTo(1).WithMessage("Campo Id não informado");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Campo Nome não informado.");
            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Campo Autor não informado.");
            RuleFor(x => x.PublisherId)
                .NotEmpty().WithMessage("Campo Editora não informado.");
            RuleFor(x => x.Release)
                .NotEmpty().WithMessage("Campo Lançamento não informado.");
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Campo Quantidade não informado.");
        }
    }
}