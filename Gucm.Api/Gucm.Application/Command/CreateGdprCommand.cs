using Gucm.Application.Validation;

namespace Gucm.Application.ViewModel
{

    public sealed class CreateGdprCommand : GdprCommand // IRequest<BusinessResult<int>>
    {
        public override bool IsValid()
        {
            var validation = new CreateGdprValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
