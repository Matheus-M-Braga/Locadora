using System;
using FluentValidation;
using Locadora.API.Models;

namespace Locadora.API.Dtos.Validations
{
    public class PublisherDtoValidator : AbstractValidator<Publishers>
    {
        public PublisherDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo Nome não informado.");
            RuleFor(x => x.City)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo Cidade não informado.");
        }
    }
}