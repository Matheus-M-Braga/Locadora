using System;
using FluentValidation;
using Locadora.API.Dtos;
using Locadora.API.Models;

namespace Locadora.API.Dtos.Validations {
    public class PublisherDtoValidator : AbstractValidator<CreatePublisherDto> {
        public PublisherDtoValidator() {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Campo Nome não informado.")
                .MinimumLength(3).WithMessage("Necessário pelo menos 3 caracteres.")
                .MaximumLength(50).WithMessage("Limite é de 50 caracteres.");
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Campo Cidade não informado.")
                .MinimumLength(3).WithMessage("Necessário pelo menos 3 caracteres.")
                .MaximumLength(50).WithMessage("Limite é de 50 caracteres.");
        }
    }

    public class UpdatePublisherDtoValidator : AbstractValidator<Publishers> {
        public UpdatePublisherDtoValidator() {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Campo Id não informado.")
                .GreaterThanOrEqualTo(1).WithMessage("Campo Id não informado");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Campo Nome não informado.")
                .MinimumLength(3).WithMessage("Necessário pelo menos 3 caracteres.")
                .MaximumLength(50).WithMessage("Limite é de 50 caracteres.");
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Campo Cidade não informado.")
                .MinimumLength(3).WithMessage("Necessário pelo menos 3 caracteres.")
                .MaximumLength(50).WithMessage("Limite é de 50 caracteres.");
        }
    }
}