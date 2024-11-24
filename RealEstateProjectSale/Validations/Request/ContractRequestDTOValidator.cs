using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;

namespace RealEstateProjectSale.Validations.Request
{
    public class ContractRequestDTOValidator : AbstractValidator<ContractRequestDTO>
    {
        public ContractRequestDTOValidator()
        {
            RuleFor(x => x.TotalPrice)
                .NotNull().WithMessage("Tổng giá trị hợp đồng là bắt buộc.")
                .GreaterThanOrEqualTo(1500000000).WithMessage("Tổng giá trị hợp đồng tối thiểu phải từ 1 tỷ 500 triệu đồng.");

            RuleFor(x => x.Description)
            .MaximumLength(2000).WithMessage("Mô tả không được vượt quá 2000 ký tự.")
            .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}
