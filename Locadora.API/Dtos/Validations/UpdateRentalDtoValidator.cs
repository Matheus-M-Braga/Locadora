using System;
using FluentValidation;

namespace Locadora.API.Dtos.Validations
{
    public class UpdateRentalDtoValidator : AbstractValidator<RentalReturnDto>
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