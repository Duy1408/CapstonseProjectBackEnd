using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Validations.Update
{
    public class BlockUpdateDTOValidator : AbstractValidator<BlockUpdateDTO>
    {
        public BlockUpdateDTOValidator()
        {
            RuleFor(x => x.BlockName)
                .MaximumLength(100).WithMessage("Tên Block không được vượt quá 100 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.BlockName));

            RuleFor(x => x.Status)
                .NotNull().WithMessage("Trạng thái không được để trống.") // Không cho phép null nếu được cung cấp
                .When(x => x.Status.HasValue);

        }
    }
}
