using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Validations.Update
{
    public class PropertyTypeUpdateDTOValidator : AbstractValidator<PropertyTypeUpdateDTO>
    {
        public PropertyTypeUpdateDTOValidator()
        {
            RuleFor(x => x.PropertyTypeName)
            .MaximumLength(100).WithMessage("Loại căn hộ không được vượt quá 100 ký tự.")
            .When(x => !string.IsNullOrEmpty(x.PropertyTypeName));
        }
    }
}
