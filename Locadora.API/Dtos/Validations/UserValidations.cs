using System;
using FluentValidation;
using Locadora.API.Models;

namespace Locadora.API.Dtos.Validations {
    public class UserDtoValidator : AbstractValidator<CreateUserDto> {
        public UserDtoValidator() {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Campo Nome não informado.")
                .MinimumLength(3).WithMessage("Necessário pelo menos 3 caracteres.")
                .MaximumLength(50).WithMessage("Limite é de 50 caracteres.");
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Campo Cidade não informado.")
                .MinimumLength(3).WithMessage("Necessário pelo menos 3 caracteres.")
                .MaximumLength(50).WithMessage("Limite é de 50 caracteres.");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Campo Endereço não informado.")
                .MinimumLength(3).WithMessage("Necessário pelo menos 3 caracteres.")
                .MaximumLength(50).WithMessage("Limite é de 50 caracteres.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Campo Email não informado.")
                .EmailAddress().WithMessage("Endereço Email inválido.")
                .MinimumLength(3).WithMessage("Necessário pelo menos 3 caracteres.")
                .MaximumLength(50).WithMessage("Limite é de 50 caracteres.");
        }
    }
    public class UpdateUserDtoValidator : AbstractValidator<Users> {
        public UpdateUserDtoValidator() {
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("Campo Id não informado.")
               .GreaterThanOrEqualTo(1).WithMessage("Campo Id não informado");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Campo Nome não informado.")
                .MinimumLength(3).WithMessage("Necessário pelo menos 3 caracteres.")
                .MaximumLength(50).WithMessage("Limite é de 50 caracteres.");
            RuleFor(x => x.City)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo Cidade não informado.")
                .MinimumLength(3).WithMessage("Necessário pelo menos 3 caracteres.")
                .MaximumLength(50).WithMessage("Limite é de 50 caracteres.");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Campo Endereço não informado.")
                .MinimumLength(3).WithMessage("Necessário pelo menos 3 caracteres.")
                .MaximumLength(50).WithMessage("Limite é de 50 caracteres.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Campo Email não informado.")
                .EmailAddress().WithMessage("Endereço Email inválido.")
                .MinimumLength(5).WithMessage("Necessário pelo menos 5 caracteres.")
                .MaximumLength(65).WithMessage("Limite é de 60 caracteres.");
        }
    }
}