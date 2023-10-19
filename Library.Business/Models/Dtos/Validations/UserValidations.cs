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
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(3, 50).WithMessage("{PropertyName}: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.City)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(3, 50).WithMessage("{PropertyName}: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.Address)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(3, 50).WithMessage("{PropertyName}: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(3, 50).WithMessage("{PropertyName}: Necessário entre 3 e 50 caracteres.")
                .EmailAddress().WithMessage("Endereço de Email inválido.");
        }
    }
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("{PropertyName}: Nâo informado.")
               .GreaterThanOrEqualTo(1).WithMessage("{PropertyName}: Não informado.");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(3, 50).WithMessage("{PropertyName}: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.City)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(3, 50).WithMessage("{PropertyName}: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.Address)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(3, 50).WithMessage("{PropertyName}: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(3, 50).WithMessage("{PropertyName}: Necessário entre 3 e 50 caracteres.")
                .EmailAddress().WithMessage("{PropertyName}: Formato inválido.");
        }
    }
}