using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;

namespace RealEstateProjectSale.Validations.Create
{
    public class OpenForSaleDetailCreateDTOValidator : AbstractValidator<OpenForSaleDetailCreateDTO>
    {
        public OpenForSaleDetailCreateDTOValidator()
        {
            RuleFor(x => x.Price)
                .NotNull().WithMessage("Giá bán là bắt buộc.")
                .GreaterThanOrEqualTo(1500000000).WithMessage("Giá bán tối thiểu phải từ 1 tỷ 500 triệu đồng.");

            RuleFor(x => x.Note)
                .MaximumLength(500).WithMessage("Ghi chú không được vượt quá 500 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Note));

        }
    }
}
