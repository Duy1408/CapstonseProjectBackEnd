using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;

namespace RealEstateProjectSale.Validations.Create
{
    public class PromotionCreateDTOValidator : AbstractValidator<PromotionCreateDTO>
    {
        public PromotionCreateDTOValidator()
        {
            RuleFor(x => x.PromotionName)
                .NotEmpty().WithMessage("Tên khuyến mãi không được để trống.")
                .MaximumLength(200).WithMessage("Tên khuyến mãi không được vượt quá 200 ký tự.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Description));

        }
    }
}
