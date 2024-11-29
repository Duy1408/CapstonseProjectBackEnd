using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;

namespace RealEstateProjectSale.Validations.Create
{
    public class ContractCreateDTOValidator : AbstractValidator<ContractCreateDTO>
    {
        public ContractCreateDTOValidator()
        {
            RuleFor(x => x.TotalPrice)
                .GreaterThanOrEqualTo(1500000000).WithMessage("Tổng giá trị hợp đồng tối thiểu phải từ 1 tỷ 500 triệu đồng.")
                .When(x => x.TotalPrice.HasValue);

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Mô tả không được vượt quá 2000 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}
