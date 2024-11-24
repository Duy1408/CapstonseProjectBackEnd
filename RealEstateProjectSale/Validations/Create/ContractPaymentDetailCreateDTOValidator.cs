using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;

namespace RealEstateProjectSale.Validations.Create
{
    public class ContractPaymentDetailCreateDTOValidator : AbstractValidator<ContractPaymentDetailCreateDTO>
    {
        public ContractPaymentDetailCreateDTOValidator()
        {
            RuleFor(x => x.PaymentRate)
                .GreaterThan(0).WithMessage("Tỷ lệ thanh toán phải lớn hơn 0.")
                .LessThanOrEqualTo(100).WithMessage("Tỷ lệ thanh toán không được vượt quá 100.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Mô tả không được vượt quá 1000 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Period)
                .NotNull().WithMessage("Thời hạn là bắt buộc.")
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Thời hạn phải lớn hơn hoặc bằng ngày hiện tại.");

            RuleFor(x => x.PaidValue)
                .NotNull().WithMessage("Số tiền đã thanh toán là bắt buộc.")
                .GreaterThanOrEqualTo(0).WithMessage("Số tiền đã thanh toán phải lớn hơn hoặc bằng 0.");

            RuleFor(x => x.PaidValueLate)
                .NotNull().WithMessage("Số tiền thanh toán trễ là bắt buộc.")
                .GreaterThanOrEqualTo(0).WithMessage("Số tiền thanh toán trễ phải lớn hơn hoặc bằng 0.");

        }
    }
}
