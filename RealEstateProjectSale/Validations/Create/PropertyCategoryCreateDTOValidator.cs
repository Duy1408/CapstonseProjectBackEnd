using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;

namespace RealEstateProjectSale.Validations.Create
{
    public class PropertyCategoryCreateDTOValidator : AbstractValidator<PropertyCategoryCreateDTO>
    {
        public PropertyCategoryCreateDTOValidator()
        {
            RuleFor(x => x.PropertyCategoryName)
            .NotEmpty().WithMessage("Loại bất động sản không được để trống.")
            .MaximumLength(100).WithMessage("Loại bất động sản không được vượt quá 100 ký tự.");

        }
    }
}
