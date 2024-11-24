using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Validations.Update
{
    public class OpenForSaleDetailUpdateDTOValidator : AbstractValidator<OpenForSaleDetailUpdateDTO>
    {
        public OpenForSaleDetailUpdateDTOValidator()
        {
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(1500000000).WithMessage("Giá bán tối thiểu phải từ 1 tỷ 500 triệu đồng.")
                .When(x => x.Price.HasValue);

            RuleFor(x => x.Note)
                .MaximumLength(500).WithMessage("Ghi chú không được vượt quá 500 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Note));
        }
    }
}
