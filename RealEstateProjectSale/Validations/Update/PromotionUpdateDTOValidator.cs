using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Validations.Update
{
    public class PromotionUpdateDTOValidator : AbstractValidator<PromotionUpdateDTO>
    {
        public PromotionUpdateDTOValidator()
        {
            RuleFor(x => x.PromotionName)
            .MaximumLength(200).WithMessage("Tên khuyến mãi không được vượt quá 200 ký tự.")
            .When(x => !string.IsNullOrEmpty(x.PromotionName));

            RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.")
            .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Status)
                .Must(status => status == null || status == true || status == false).WithMessage("Trạng thái không hợp lệ nếu có.");

        }
    }
}
