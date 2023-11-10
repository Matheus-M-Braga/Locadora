using FluentValidation;
using Library.Business.Models.Dtos.Rental;

namespace Library.Business.Models.Dtos.Validations
{
    public class RentalDtoValidator : AbstractValidator<CreateRentalDto>
    {
        public RentalDtoValidator()
        {
            RuleFor(x => x.BookId)
                .NotEmpty().WithMessage("Livro: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("Livro: Não informado.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Usuário: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("Usuário: Não informado.");

            RuleFor(x => x.RentalDate)
                .NotEmpty().WithMessage("Data do Alugel: Não informado.");

            RuleFor(x => x.ForecastDate)
                .NotEmpty().WithMessage("Data de Previsão: Não informado.");
        }
    }

    public class UpdateRentalDtoValidator : AbstractValidator<UpdateRentalDto>
    {
        public UpdateRentalDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("Id: Não informado.");

            RuleFor(x => x.ReturnDate)
                .NotEmpty().WithMessage("Data de Devolução: Não informado.");
        }
    }
}