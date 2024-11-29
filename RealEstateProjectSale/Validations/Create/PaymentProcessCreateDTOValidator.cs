using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;

namespace RealEstateProjectSale.Validations.Create
{
    public class PaymentProcessCreateDTOValidator : AbstractValidator<PaymentProcessCreateDTO>
    {
        public PaymentProcessCreateDTOValidator()
        {
            RuleFor(x => x.PaymentProcessName)
            .NotEmpty().WithMessage("Tên phương thức thanh toán là bắt buộc.")
            .MaximumLength(100).WithMessage("Tên phương thức thanh toán không được vượt quá 100 ký tự.");


        }
    }
}
