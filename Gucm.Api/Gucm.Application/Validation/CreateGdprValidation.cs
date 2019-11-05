using FluentValidation;
using Gucm.Application.ViewModel;

namespace Gucm.Application.Validation
{
    public class CreateGdprValidation : AbstractValidator<CreateGdprCommand>
    {
        public CreateGdprValidation()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Please select Id.")
                .GreaterThanOrEqualTo(0).WithMessage("Test must not be less than or equal to 0");

            RuleFor(x => x.Gdpr).NotEmpty().WithMessage("Please select a Gdpr.");
        }
    }
}
