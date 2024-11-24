using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Validations.Update
{
    public class CommentUpdateDTOValidator : AbstractValidator<CommentUpdateDTO>
    {
        public CommentUpdateDTOValidator()
        {
            RuleFor(x => x.Content)
                .MaximumLength(1000).WithMessage("Nội dung bình luận không được vượt quá 1000 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Content));

            RuleFor(x => x.Status)
                .NotNull().WithMessage("Trạng thái không được để trống.")
                .When(x => x.Status.HasValue);
        }
    }
}
