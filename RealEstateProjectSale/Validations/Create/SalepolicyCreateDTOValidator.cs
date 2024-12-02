using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;

namespace RealEstateProjectSale.Validations.Create
{
    public class SalepolicyCreateDTOValidator : AbstractValidator<SalepolicyCreateDTO>
    {
        public SalepolicyCreateDTOValidator()
        {
            RuleFor(x => x.SalesPolicyType)
              .NotEmpty().WithMessage("Loại chính sách bán hàng là bắt buộc.")
              .MaximumLength(100).WithMessage("Loại chính sách bán hàng không được vượt quá 100 ký tự.");

            RuleFor(x => x.ExpressTime)
              .NotEmpty().WithMessage("Ngày áp dụng là bắt buộc.")
              .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Ngày chọn phải từ hôm nay trở về sau.");

            RuleFor(x => x.PeopleApplied)
              .MaximumLength(100).WithMessage("Loại chính sách bán hàng không được vượt quá 1000 ký tự.")
              .When(x => !string.IsNullOrEmpty(x.PeopleApplied));

            RuleFor(x => x.Status)
                .Must(status => status == null || status == true || status == false).WithMessage("Trạng thái không hợp lệ nếu có.");


        }
    }
}
