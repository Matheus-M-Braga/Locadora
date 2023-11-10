using FluentValidation;
using Library.Business.Models.Dtos.Publisher;

namespace Library.Business.Models.Dtos.Validations
{
    public class PublisherDtoValidator : AbstractValidator<CreatePublisherDto>
    {
        public PublisherDtoValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Nome: Não informado.")
                .Length(3, 50).WithMessage("Nome: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.City)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Cidade: Não informado.")
                .Length(3, 50).WithMessage("Cidade: Necessário entre 3 e 50 caracteres.");
        }
    }

    public class UpdatePublisherDtoValidator : AbstractValidator<UpdatePublisherDto>
    {
        public UpdatePublisherDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id: Não informado.")
                .GreaterThanOrEqualTo(1).WithMessage("Id: Não informado");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Nome: Não informado.")
                .Length(3, 50).WithMessage("Nome: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.City)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Cidade: Não informado.")
                .Length(3, 50).WithMessage("Cidade: Necessário entre 3 e 50 caracteres.");
        }
    }
}