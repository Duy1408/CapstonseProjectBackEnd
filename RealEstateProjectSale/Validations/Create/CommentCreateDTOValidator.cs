using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;

namespace RealEstateProjectSale.Validations.Create
{
    public class CommentCreateDTOValidator : AbstractValidator<CommentCreateDTO>
    {
        public CommentCreateDTOValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Nội dung bình luận là bắt buộc.")
                .MaximumLength(1000).WithMessage("Nội dung bình luận không được vượt quá 1000 ký tự.");

        }
    }
}
