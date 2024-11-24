using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Request;

namespace RealEstateProjectSale.Validations.Request
{
    public class BlockRequestDTOValidator : AbstractValidator<BlockRequestDTO>
    {
        public BlockRequestDTOValidator()
        {
            RuleFor(x => x.BlockName)
                .NotEmpty().WithMessage("Tên Block là bắt buộc.")
                .MaximumLength(100).WithMessage("Tên Block không được vượt quá 100 ký tự.");

        }
    }
}
