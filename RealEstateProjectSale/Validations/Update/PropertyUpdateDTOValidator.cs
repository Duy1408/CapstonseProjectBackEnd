using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.Enums;

namespace RealEstateProjectSale.Validations.Update
{
    public class PropertyUpdateDTOValidator : AbstractValidator<PropertyUpdateDTO>
    {
        public PropertyUpdateDTOValidator()
        {
            RuleFor(x => x.PropertyCode)
                .MinimumLength(4).WithMessage("Mã bất động sản phải có ít nhất 4 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.PropertyCode));

            RuleFor(x => x.PriceSold)
                .GreaterThanOrEqualTo(1500000000).WithMessage("Giá bán tối thiểu phải từ 1 tỷ 500 triệu đồng.")
                .When(x => x.PriceSold.HasValue);

            RuleFor(x => x.View)
                .MaximumLength(500).WithMessage("Tiện ích cảnh quan không được vượt quá 500 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.View));

            RuleFor(x => x.Status)
                .Must(status =>
                {
                    if (string.IsNullOrEmpty(status)) return true;
                    return int.TryParse(status, out var value) && Enum.IsDefined(typeof(PropertyStatus), value);
                })
                .WithMessage("Trạng thái không hợp lệ. Vui lòng nhập số tương ứng với enum PropertyStatus.");

        }
    }
}
