using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Validations.Update
{
    public class ZoneUpdateDTOValidator : AbstractValidator<ZoneUpdateDTO>
    {
        public ZoneUpdateDTOValidator()
        {
            RuleFor(x => x.ZoneName)
            .MaximumLength(100).WithMessage("Tên Zone không được vượt quá 100 ký tự.")
            .When(x => !string.IsNullOrEmpty(x.ZoneName));

            RuleFor(x => x.Status)
                .Must(status => status == null || status == true || status == false).WithMessage("Trạng thái không hợp lệ nếu có.");
        }
    }
}
