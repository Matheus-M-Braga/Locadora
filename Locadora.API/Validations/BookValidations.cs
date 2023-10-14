using FluentValidation;
using Locadora.API.Dtos.Book;

namespace Locadora.API.Validations
{
    public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookDtoValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(3, 50).WithMessage("{PropertyName}: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.Author)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(3, 50).WithMessage("{PropertyName}: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.PublisherId)
                .NotEmpty().WithMessage("{PropertyName}: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("{PropetyName}: Não informado.");

            RuleFor(x => x.Release)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(4).WithMessage("{PropertyName}: Necessário 4 caracteres.");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("{PropertyName}: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("{PropetyName}: Não informado.");
        }
    }

    public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("{PropertyName}: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("{PropetyName}: Não informado.");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(3, 50).WithMessage("{PropertyName}: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.Author)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(3, 50).WithMessage("{PropertyName}: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.PublisherId)
                .NotEmpty().WithMessage("{PropertyName}: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("{PropetyName}: Não informado.");

            RuleFor(x => x.Release)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(4).WithMessage("{PropertyName}: Necessário 4 caracteres.");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("{PropertyName}: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("{PropetyName}: Não informado.");
        }
    }
}