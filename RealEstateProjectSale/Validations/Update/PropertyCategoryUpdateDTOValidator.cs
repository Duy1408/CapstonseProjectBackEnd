using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Validations.Update
{
    public class PropertyCategoryUpdateDTOValidator : AbstractValidator<PropertyCategoryUpdateDTO>
    {
        public PropertyCategoryUpdateDTOValidator()
        {
            RuleFor(x => x.PropertyCategoryName)
            .MaximumLength(100).WithMessage("Loại bất động sản không được vượt quá 100 ký tự.")
            .When(x => !string.IsNullOrEmpty(x.PropertyCategoryName));

        }
    }
}
