using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Validations.Update
{
    public class PromotionDetailUpdateDTOValidator : AbstractValidator<PromotionDetailUpdateDTO>
    {
        public PromotionDetailUpdateDTOValidator()
        {
            RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.")
            .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Số tiền khuyến mãi phải lớn hơn 0.")
            .When(x => x.Amount.HasValue);

        }
    }
}
