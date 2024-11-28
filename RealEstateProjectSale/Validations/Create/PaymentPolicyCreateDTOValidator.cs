using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;

namespace RealEstateProjectSale.Validations.Create
{
    public class PaymentPolicyCreateDTOValidator : AbstractValidator<PaymentPolicyCreateDTO>
    {
        public PaymentPolicyCreateDTOValidator()
        {
            RuleFor(x => x.PaymentPolicyName)
                .NotEmpty().WithMessage("Tên chính sách thanh toán là bắt buộc.")
                .MaximumLength(100).WithMessage("Tên chính sách thanh toán không được vượt quá 100 ký tự.");

            RuleFor(x => x.LateDate)
                .GreaterThanOrEqualTo(0).WithMessage("Số ngày trễ phải lớn hơn hoặc bằng 0.")
                .When(x => x.LateDate.HasValue);

            RuleFor(x => x.PercentLate)
                .GreaterThanOrEqualTo(0).WithMessage("Phần trăm trễ phải lớn hơn hoặc bằng 0.")
                .LessThanOrEqualTo(1).WithMessage("Phần trăm trễ không được vượt quá 1.")
                .When(x => x.PercentLate.HasValue);

        }
    }
}
