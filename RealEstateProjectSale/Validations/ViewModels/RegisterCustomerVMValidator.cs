using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.ViewModels;

namespace RealEstateProjectSale.Validations.ViewModels
{
    public class RegisterCustomerVMValidator : AbstractValidator<RegisterCustomerVM>
    {
        public RegisterCustomerVMValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email là bắt buộc.")
                .EmailAddress().WithMessage("Địa chỉ email không hợp lệ.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu là bắt buộc.")
                .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự.");

            RuleFor(x => x.ConfirmPass)
                .NotEmpty().WithMessage("Xác nhận mật khẩu là bắt buộc.")
                .Equal(x => x.Password).WithMessage("Mật khẩu và xác nhận mật khẩu không khớp.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Họ và tên là bắt buộc.")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Họ và tên chỉ được chứa chữ cái và khoảng trắng.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Ngày sinh là bắt buộc.")
                .LessThan(DateTime.Today).WithMessage("Ngày sinh phải là ngày trong quá khứ.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Số điện thoại là bắt buộc.")
                .Matches(@"^\d{7,11}$").WithMessage("Số điện thoại phải từ 07 đến 11 chữ số.");

            RuleFor(x => x.IdentityCardNumber)
                .Matches(@"^\d{6,12}$").When(x => !string.IsNullOrEmpty(x.IdentityCardNumber))
                .WithMessage("Số CMND phải từ 6 đến 12 chữ số.");

            RuleFor(x => x.Nationality)
                .NotEmpty().WithMessage("Quốc tịch là bắt buộc.")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Họ và tên chỉ được chứa chữ cái và khoảng trắng.");

            RuleFor(x => x.PlaceofOrigin)
                .Matches(@"^[a-zA-Z\s]*$").WithMessage("Nơi sinh chỉ được chứa chữ cái và khoảng trắng.")
                .When(x => !string.IsNullOrEmpty(x.PlaceofOrigin));

            RuleFor(x => x.PlaceOfResidence)
                .Matches(@"^[a-zA-Z\s]*$").WithMessage("Nơi cư trú chỉ được chứa chữ cái và khoảng trắng.")
                .When(x => !string.IsNullOrEmpty(x.PlaceOfResidence));

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Địa chỉ là bắt buộc.")
                .MaximumLength(500).WithMessage("Địa chỉ không được vượt quá 500 ký tự.");

            RuleFor(x => x.BankName)
                .Matches(@"^[a-zA-Z\s]*$").WithMessage("Nơi cư trú chỉ được chứa chữ cái và khoảng trắng.")
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.BankNumber))
                .WithMessage("Tên ngân hàng là bắt buộc khi có số tài khoản.");

            RuleFor(x => x.BankNumber)
                .Matches(@"^\d{6,20}$").When(x => !string.IsNullOrEmpty(x.BankNumber))
                .WithMessage("Số tài khoản phải từ 6 đến 20 chữ số.");

            RuleFor(x => x.DateOfExpiry)
                .Matches(@"^\d{2}-\d{2}-\d{4}$").When(x => !string.IsNullOrEmpty(x.DateOfExpiry))
                .WithMessage("Ngày hết hạn phải theo định dạng MM-dd-yyyy.");

            RuleFor(x => x.Taxcode)
                .Matches(@"^\d{10}$").When(x => !string.IsNullOrEmpty(x.Taxcode))
                .WithMessage("Mã số thuế phải là 10 chữ số.");

        }
    }
}
