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
                .NotEmpty().WithMessage("Nome: Não informado.")
                .Length(3, 50).WithMessage("Nome: Limite é de 50 caracteres.");

            RuleFor(x => x.Author)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Autor: Não informado.")
                .Length(3, 50).WithMessage("Autor: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.PublisherId)
                .NotEmpty().WithMessage("Editora: Nâo informada.")
                .GreaterThanOrEqualTo(1).WithMessage("Editora: Não informada.");

            RuleFor(x => x.Release)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Lançamento: Não informado.")
                .Must(x => x >= 1000 && x <= 9999).WithMessage("Lançamento: Necessário 4 dígitos.");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantidade: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("Quantidade: Não informado.");
        }
    }

    public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("Id: Não informado.");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Nome: Não informado.")
                .Length(3, 50).WithMessage("Nome: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.Author)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Autor: Não informado.")
                .Length(3, 50).WithMessage("Autor: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.PublisherId)
                .NotEmpty().WithMessage("Editora: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("Editora: Não informado.");

            RuleFor(x => x.Release)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Lançamento: Não informado.")
                .Must(x => x >= 1000 && x <= 9999).WithMessage("Lançamento: Necessário 4 dígitos.");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantidade: Nâo informado.")
                .GreaterThanOrEqualTo(0).WithMessage("Quantidade: Não informado.");
        }
    }
}