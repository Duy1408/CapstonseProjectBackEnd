using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Validations.Update
{
    public class NotificationUpdateDTOValidator : AbstractValidator<NotificationUpdateDTO>
    {
        public NotificationUpdateDTOValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(200).WithMessage("Tiêu đề không được dài quá 200 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Title));

            RuleFor(x => x.Subtiltle)
                .MaximumLength(200).WithMessage("Phụ đề không được dài quá 200 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Subtiltle));

            RuleFor(x => x.Body)
                .MaximumLength(1000).WithMessage("Nội dung không được dài quá 1000 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Body));

            RuleFor(x => x.Status)
                .Must(status => status == null || status == true || status == false).WithMessage("Trạng thái không hợp lệ nếu có.");

        }
    }
}
