using FluentValidation;

namespace Locadora.API.Dtos.Validations
{
    public class RentalDtoValidator : AbstractValidator<CreateRentalDto>
    {
        public RentalDtoValidator()
        {
            RuleFor(x => x.BookId)
                .NotEmpty().WithMessage("{PropertyName}: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("{PropetyName}: Não informado.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("{PropertyName}: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("{PropetyName}: Não informado.");

            RuleFor(x => x.RentalDate)
                .NotEmpty().WithMessage("{PropetyName}: Não informado.");

            RuleFor(x => x.ForecastDate)
                .NotEmpty().WithMessage("{PropetyName}: Não informado.");
        }
    }

    public class UpdateRentalDtoValidator : AbstractValidator<UpdateRentalDto>
    {
        public UpdateRentalDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("{PropertyName}: Nâo informado.")
                .GreaterThanOrEqualTo(1).WithMessage("{PropetyName}: Não informado.");
                
            RuleFor(x => x.ReturnDate)
                .NotEmpty().WithMessage("{PropetyName}: Não informado.");
        }
    }
}