using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSale.Validations.Create
{
    public class AccountCreateDTOValidator : AbstractValidator<AccountCreateDTO>
    {
        public AccountCreateDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email là bắt buộc.")
                .EmailAddress().WithMessage("Định dạng Email không hợp lệ.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu là bắt buộc.")
                .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự.");

        }
    }
}
