using FluentValidation;
using Locadora.API.Dtos;
using Locadora.API.Dtos.Rental;

namespace Locadora.API.Validations
{
    public class RentalDtoValidator : AbstractValidator<CreateRentalDto>
    {
        public RentalDtoValidator()
        {
            RuleFor(x => x.BookId)
                .NotEmpty().WithMessage("{PropertyName}: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName}: Não informado.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("{PropertyName}: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName}: Não informado.");

            RuleFor(x => x.RentalDate)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.");

            RuleFor(x => x.ForecastDate)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.");
        }
    }

    public class UpdateRentalDtoValidator : AbstractValidator<UpdateRentalDto>
    {
        public UpdateRentalDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("{PropertyName}: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName}: Não informado.");

            RuleFor(x => x.ReturnDate)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.");
        }
    }
}