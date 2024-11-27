using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Validations.Update
{
    public class AccountUpdateDTOValidator : AbstractValidator<AccountUpdateDTO>
    {
        public AccountUpdateDTOValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Định dạng Email không hợp lệ.")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Password)
                .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Password));

            RuleFor(x => x.Status)
                .Must(status => status == null || status == true || status == false).WithMessage("Trạng thái không hợp lệ nếu có.");

        }
    }
}
