using FluentValidation;
using Library.Business.Models.Dtos.LoginUser;

namespace Library.Business.Models.Dtos.Validations
{
    public class LoginUserCreateDtoValidator : AbstractValidator<LoginUserCreateDto>
    {
        public LoginUserCreateDtoValidator()
        {
            RuleFor(x => x.Name)
              .Cascade(CascadeMode.Stop)
              .NotEmpty().WithMessage("Nome: Não informado.")
              .Length(3, 50).WithMessage("Nome: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email: Não informado.")
                .Length(3, 50).WithMessage("Email: Necessário entre 3 e 50 caracteres.")
                .EmailAddress().WithMessage("Endereço de Email inválido.");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Senha: Não informado.")
                .MinimumLength(8).WithMessage("Senha: Necessário pelo menos 8 caracteres.");
        }
    }

    public class LoginUserUpdateDtoValidator : AbstractValidator<LoginUserUpdateDto>
    {
        public LoginUserUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("Id: Nâo informado.")
               .GreaterThanOrEqualTo(1).WithMessage("Id: Não informado.");

            RuleFor(x => x.Name)
              .Cascade(CascadeMode.Stop)
              .NotEmpty().WithMessage("Nome: Não informado.")
              .Length(3, 50).WithMessage("Nome: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email: Não informado.")
                .Length(3, 50).WithMessage("Email: Necessário entre 3 e 50 caracteres.")
                .EmailAddress().WithMessage("Endereço de Email inválido.");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Senha: Não informado.")
                .MinimumLength(8).WithMessage("Senha: Necessário pelo menos 8 caracteres.");
        }
    }
}
