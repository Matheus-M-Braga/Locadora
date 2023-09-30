using System;
using FluentValidation;

namespace Locadora.API.Dtos.Validations
{
    public class RentalDtoValidator : AbstractValidator<CreateRentalDto>
    {
        public RentalDtoValidator()
        {
            RuleFor(x => x.BookId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo Livro não informado.");
            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo Usuário não informado.");
            RuleFor(x => x.RentalDate)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo Data Aluguel não informado.");
            RuleFor(x => x.ForecastDate)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo Data Previsão não informado.");
        }
    }

    public class UpdateRentalDtoValidator : AbstractValidator<UpdateRentalDto>
    {
        public UpdateRentalDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo Id não informado.");
            RuleFor(x => x.ReturnDate)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo Data de Devolução não informado.");
        }
    }
}