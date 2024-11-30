using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;

namespace RealEstateProjectSale.Validations.Create
{
    public class PromotionDetailCreateDTOValidator : AbstractValidator<PromotionDetailCreateDTO>
    {
        public PromotionDetailCreateDTOValidator()
        {
            RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Mô tả không được để trống.")
            .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.");

            RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Số tiền khuyến mãi phải lớn hơn 0.")
            .When(x => x.Amount.HasValue);


        }
    }
}
