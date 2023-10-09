using System;
using FluentValidation;

namespace Locadora.API.Dtos.Validations
{
    public class RentalDtoValidator : AbstractValidator<CreateRentalDto>
    {
        public RentalDtoValidator()
        {
            RuleFor(x => x.BookId)
                .NotEmpty().WithMessage("Campo Livro não informado.")
                .GreaterThanOrEqualTo(1).WithMessage("Campo Livro não informado.");
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Campo Livro não informado.")
                .GreaterThanOrEqualTo(1).WithMessage("Campo Usuário não informado.");
            RuleFor(x => x.RentalDate)
            
                .NotEmpty().WithMessage("Campo Data Aluguel não informado.");
            RuleFor(x => x.ForecastDate)
                .NotEmpty().WithMessage("Campo Data Previsão não informado.");
        }
    }

    public class UpdateRentalDtoValidator : AbstractValidator<UpdateRentalDto>
    {
        public UpdateRentalDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Campo Id não informado.")
                .GreaterThanOrEqualTo(1).WithMessage("Campo Id não informado");
            RuleFor(x => x.ReturnDate)
                .NotEmpty().WithMessage("Campo Data de Devolução não informado.");
        }
    }
}