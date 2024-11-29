using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;

namespace RealEstateProjectSale.Validations.Create
{
    public class PaymentProcessDetailCreateDTOValidator : AbstractValidator<PaymentProcessDetailCreateDTO>
    {
        public PaymentProcessDetailCreateDTOValidator()
        {
            RuleFor(x => x.PaymentStage)
                .NotEmpty().WithMessage("Giai đoạn thanh toán là bắt buộc.")
                .GreaterThan(0).WithMessage("Giai đoạn thanh toán phải lớn hơn 0.")
                .LessThanOrEqualTo(50).WithMessage("Số giai đoạn thanh toán không thể vượt quá 50.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.DurationDate)
                .GreaterThanOrEqualTo(0).WithMessage("Số ngày phải lớn hơn hoặc bằng 0.")
                .When(x => x.DurationDate.HasValue);

            RuleFor(x => x.Percentage)
                .GreaterThanOrEqualTo(0).WithMessage("Phần trăm phải lớn hơn hoặc bằng 0.")
                .LessThanOrEqualTo(1).WithMessage("Phần trăm không được vượt quá 1.")
                .When(x => x.Percentage.HasValue);

            RuleFor(x => x.Amount)
                .GreaterThanOrEqualTo(0).WithMessage("Số tiền phải lớn hơn hoặc bằng 0.")
                .When(x => x.Amount.HasValue);


        }
    }
}
