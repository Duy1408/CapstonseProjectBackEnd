using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Validations.Update
{
    public class PaymentPolicyUpdateDTOValidator : AbstractValidator<PaymentPolicyUpdateDTO>
    {
        public PaymentPolicyUpdateDTOValidator()
        {
            RuleFor(x => x.PaymentPolicyName)
                .NotEmpty().WithMessage("Tên chính sách thanh toán không được để trống.")
                .MaximumLength(100).WithMessage("Tên chính sách thanh toán không được vượt quá 100 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.PaymentPolicyName));

            RuleFor(x => x.LateDate)
                .GreaterThanOrEqualTo(0).WithMessage("Số ngày trễ phải lớn hơn hoặc bằng 0.")
                .When(x => x.LateDate.HasValue);

            RuleFor(x => x.PercentLate)
                .GreaterThanOrEqualTo(0).WithMessage("Phần trăm trễ phải lớn hơn hoặc bằng 0.")
                .LessThanOrEqualTo(1).WithMessage("Phần trăm trễ không được vượt quá 1.")
                .When(x => x.PercentLate.HasValue);

            RuleFor(x => x.Status)
                .Must(status => status == null || status == true || status == false).WithMessage("Trạng thái không hợp lệ nếu có.");

        }
    }
}
