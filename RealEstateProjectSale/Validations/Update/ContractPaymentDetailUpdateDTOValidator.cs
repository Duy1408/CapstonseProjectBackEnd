using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Validations.Update
{
    public class ContractPaymentDetailUpdateDTOValidator : AbstractValidator<ContractPaymentDetailUpdateDTO>
    {
        public ContractPaymentDetailUpdateDTOValidator()
        {
            RuleFor(x => x.PaymentRate)
                .GreaterThan(0).WithMessage("Tỷ lệ thanh toán phải lớn hơn 0.")
                .LessThanOrEqualTo(100).WithMessage("Tỷ lệ thanh toán không được vượt quá 100.")
                .When(x => x.PaymentRate.HasValue);

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Mô tả không được vượt quá 1000 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Period)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Thời hạn phải lớn hơn hoặc bằng ngày hiện tại.")
                .When(x => x.Period.HasValue);

            RuleFor(x => x.PaidValue)
                .GreaterThanOrEqualTo(0).WithMessage("Số tiền đã thanh toán phải lớn hơn hoặc bằng 0.")
                .When(x => x.PaidValue.HasValue);

            RuleFor(x => x.PaidValueLate)
                .GreaterThanOrEqualTo(0).WithMessage("Số tiền thanh toán trễ phải lớn hơn hoặc bằng 0.")
                .When(x => x.PaidValueLate.HasValue);

            RuleFor(x => x.Status)
                .NotNull().WithMessage("Trạng thái không được để trống.")
                .When(x => x.Status.HasValue);

        }
    }
}
