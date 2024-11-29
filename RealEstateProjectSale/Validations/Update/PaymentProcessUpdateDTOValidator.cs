using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Validations.Update
{
    public class PaymentProcessUpdateDTOValidator : AbstractValidator<PaymentProcessUpdateDTO>
    {
        public PaymentProcessUpdateDTOValidator()
        {
            RuleFor(x => x.PaymentProcessName)
            .MaximumLength(100).WithMessage("Tên quy trình thanh toán không được vượt quá 100 ký tự.")
            .When(x => !string.IsNullOrEmpty(x.PaymentProcessName));

            RuleFor(x => x.Status)
                .Must(status => status == null || status == true || status == false).WithMessage("Trạng thái không hợp lệ nếu có.");



        }
    }
}
