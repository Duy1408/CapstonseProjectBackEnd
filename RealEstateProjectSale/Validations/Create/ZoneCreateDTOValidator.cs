using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Validations.Create
{
    public class ZoneCreateDTOValidator : AbstractValidator<ZoneCreateDTO>
    {

        public ZoneCreateDTOValidator()
        {
            RuleFor(x => x.ZoneName)
               .MaximumLength(100).WithMessage("Tên Zone không được vượt quá 100 ký tự.")
               .When(x => !string.IsNullOrEmpty(x.ZoneName));


            RuleFor(x => x.Status)
                .Must(status => status == null || status == true || status == false).WithMessage("Trạng thái không hợp lệ nếu có.");
        }
    }
}
