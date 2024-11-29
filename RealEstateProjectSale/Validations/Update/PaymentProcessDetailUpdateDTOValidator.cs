using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Validations.Update
{
    public class PaymentProcessDetailUpdateDTOValidator : AbstractValidator<PaymentProcessDetailUpdateDTO>
    {
        public PaymentProcessDetailUpdateDTOValidator()
        {
            RuleFor(x => x.PaymentStage)
            .GreaterThan(0).WithMessage("Giai đoạn thanh toán phải lớn hơn 0.")
            .LessThanOrEqualTo(50).WithMessage("Số giai đoạn thanh toán không thể vượt quá 50.")
            .When(x => x.PaymentStage.HasValue);

            RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.")
            .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Percentage)
            .GreaterThanOrEqualTo(0).WithMessage("Phần trăm phải lớn hơn hoặc bằng 0.")
            .LessThanOrEqualTo(1).WithMessage("Phần trăm không được vượt quá 1.")
            .When(x => x.Percentage.HasValue);

            RuleFor(x => x.DurationDate)
            .GreaterThanOrEqualTo(0).WithMessage("Số ngày phải lớn hơn hoặc bằng 0.")
            .When(x => x.DurationDate.HasValue);

            RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0).WithMessage("Số tiền phải lớn hơn hoặc bằng 0.")
            .When(x => x.Amount.HasValue);


        }
    }
}
