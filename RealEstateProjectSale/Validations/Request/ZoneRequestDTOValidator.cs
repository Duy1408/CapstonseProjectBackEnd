using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Request;

namespace RealEstateProjectSale.Validations.Request
{
    public class ZoneRequestDTOValidator : AbstractValidator<ZoneRequestDTO>
    {
        public ZoneRequestDTOValidator()
        {
            RuleFor(x => x.ZoneName)
                .NotEmpty().WithMessage("Tên Zone là bắt buộc.")
                .MaximumLength(100).WithMessage("Tên Zone không được vượt quá 100 ký tự.");
        }
    }
}
