using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;

namespace RealEstateProjectSale.Validations.Create
{
    public class PropertyTypeCreateDTOValidator : AbstractValidator<PropertyTypeCreateDTO>
    {
        public PropertyTypeCreateDTOValidator()
        {
            RuleFor(x => x.PropertyTypeName)
             .NotEmpty().WithMessage("Loại căn hộ là bắt buộc.")
             .MaximumLength(100).WithMessage("Loại căn hộ không được vượt quá 100 ký tự.");
        }
    }
}
