using FluentValidation;
using Library.Business.Models.Dtos.User;

namespace Library.Business.Models.Dtos.Validations
{
    public class UserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Nome: Não informado.")
                .Length(3, 50).WithMessage("Nome: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.City)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Cidade: Não informado.")
                .Length(3, 50).WithMessage("Cidade: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.Address)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Endereço: Não informado.")
                .Length(3, 50).WithMessage("Endereço: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email: Não informado.")
                .Length(3, 50).WithMessage("Email: Necessário entre 3 e 50 caracteres.")
                .EmailAddress().WithMessage("Endereço de Email inválido.");
        }
    }
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("Id: Nâo informado.")
               .GreaterThanOrEqualTo(1).WithMessage("Id: Não informado.");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Nome: Não informado.")
                .Length(3, 50).WithMessage("Nome: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.City)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Cidade: Não informado.")
                .Length(3, 50).WithMessage("Cidade: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.Address)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Endereço: Não informado.")
                .Length(3, 50).WithMessage("Endereço: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email: Não informado.")
                .Length(3, 50).WithMessage("Email: Necessário entre 3 e 50 caracteres.")
                .EmailAddress().WithMessage("Endereço de Email inválido.");
        }
    }
}