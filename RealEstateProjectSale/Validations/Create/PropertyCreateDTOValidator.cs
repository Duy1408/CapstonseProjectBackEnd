using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;

namespace RealEstateProjectSale.Validations.Create
{
    public class PropertyCreateDTOValidator : AbstractValidator<PropertyCreateDTO>
    {
        public PropertyCreateDTOValidator()
        {
            RuleFor(x => x.PropertyCode)
                .NotEmpty().WithMessage("Mã căn hộ là bắt buộc.")
                .MinimumLength(4).WithMessage("Mã căn hộ phải có ít nhất 4 ký tự.");

            RuleFor(x => x.PriceSold)
                .GreaterThanOrEqualTo(1500000000).WithMessage("Giá bán tối thiểu phải từ 1 tỷ 500 triệu đồng.")
                .When(x => x.PriceSold.HasValue);

            RuleFor(x => x.View)
                .MaximumLength(500).WithMessage("Tiện ích cảnh quan không được vượt quá 500 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.View));

            RuleFor(x => x.UnitTypeID)
                .NotEmpty().WithMessage("UnitTypeID là bắt buộc.");

            RuleFor(x => x.ZoneID)
                .NotEmpty().WithMessage("ZoneID là bắt buộc.");

            RuleFor(x => x.ProjectCategoryDetailID)
                .NotEmpty().WithMessage("ProjectCategoryDetailID là bắt buộc.");

        }
    }
}
