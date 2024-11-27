using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;

namespace RealEstateProjectSale.Validations.Create
{
    public class NotificationCreateDTOValidator : AbstractValidator<NotificationCreateDTO>
    {
        public NotificationCreateDTOValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Tiêu đề không được để trống.")
                .MaximumLength(200).WithMessage("Tiêu đề không được dài quá 200 ký tự.");

            RuleFor(x => x.Subtiltle)
                .NotEmpty().WithMessage("Phụ đề không được để trống.")
                .MaximumLength(200).WithMessage("Phụ đề không được dài quá 200 ký tự.");

            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Nội dung không được để trống.")
                .MaximumLength(1000).WithMessage("Nội dung không được dài quá 1000 ký tự.");

        }
    }
}
