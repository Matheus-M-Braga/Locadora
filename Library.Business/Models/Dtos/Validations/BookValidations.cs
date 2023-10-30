using FluentValidation;
using Library.Business.Models.Dtos.Book;

namespace Library.Business.Models.Dtos.Validations
{
    public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookDtoValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(3, 50).WithMessage("{PropertyName}: Limite é de 50 caracteres.");

            RuleFor(x => x.Author)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(3, 50).WithMessage("{PropertyName}: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.PublisherId)
                .NotEmpty().WithMessage("{PropertyName}: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName}: Não informado.");

            RuleFor(x => x.Release)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Must(x => x >= 1000 && x <= 9999).WithMessage("{PropertyName}: Necessário 4 dígitos.");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("{PropertyName}: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName}: Não informado.");
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
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName}: Não informado.");

            RuleFor(x => x.Release)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Must(x => x >= 1000 && x <= 9999).WithMessage("{PropertyName}: Necessário 4 dígitos.");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("{PropertyName}: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName}: Não informado.");
        }
    }
}