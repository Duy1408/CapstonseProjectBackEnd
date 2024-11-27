using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Validations.Update
{
    public class PaymentUpdateDTOValidator : AbstractValidator<PaymentUpdateDTO>
    {
        public PaymentUpdateDTOValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Số tiền phải lớn hơn 0.")
                .When(x => x.Amount.HasValue);

            RuleFor(x => x.Content)
                .MaximumLength(500).WithMessage("Nội dung không được dài quá 500 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Content));

            RuleFor(x => x.Status)
                .Must(status => status == null || status == true || status == false).WithMessage("Trạng thái không hợp lệ nếu có.");

        }
    }
}
