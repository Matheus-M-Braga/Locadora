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
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(3, 50).WithMessage("{PropertyName}: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.City)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(3, 50).WithMessage("{PropertyName}: Necessário entre 3 e 50 caracteres.");
        }
    }

    public class UpdatePublisherDtoValidator : AbstractValidator<UpdatePublisherDto>
    {
        public UpdatePublisherDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Campo Id não informado.")
                .GreaterThanOrEqualTo(1).WithMessage("Campo Id não informado");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(3, 50).WithMessage("{PropertyName}: Necessário entre 3 e 50 caracteres.");

            RuleFor(x => x.City)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName}: Não informado.")
                .Length(3, 50).WithMessage("{PropertyName}: Necessário entre 3 e 50 caracteres.");
        }
    }
}