using System;
using FluentValidation;
using Locadora.API.Models;

namespace Locadora.API.Dtos.Validations
{
    public class UserDtoValidator : AbstractValidator<Users>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo Nome não informado.");
            RuleFor(x => x.City)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo Cidade não informado.");
            RuleFor(x => x.Address)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo Endereço não informado.");
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo Email não informado.");
        }
    }
}