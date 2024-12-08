using FluentValidation;
using RealEstateProjectSaleBusinessObject.ViewModels;

namespace RealEstateProjectSale.Validations.ViewModels
{
    public class RegisterStaffVMValidator : AbstractValidator<RegisterStaffVM>
    {
        public RegisterStaffVMValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email là bắt buộc.")
                .EmailAddress().WithMessage("Email không hợp lệ.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@securenest\.com$").WithMessage("Email phải có tên miền là securenest.com."); ;

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu là bắt buộc.")
                .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự.");

            RuleFor(x => x.ConfirmPass)
                .NotEmpty().WithMessage("Xác nhận mật khẩu là bắt buộc.")
                .Equal(x => x.Password).WithMessage("Mật khẩu và xác nhận mật khẩu không khớp.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Họ và tên là bắt buộc.")
                .Matches(@"^[\p{L}\s]+$").WithMessage("Họ và tên chỉ được chứa chữ cái và khoảng trắng.");

            RuleFor(x => x.PersonalEmail)
                .NotEmpty().WithMessage("Email cá nhân là bắt buộc.")
                .EmailAddress().WithMessage("Địa chỉ Email cá nhân không hợp lệ.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Ngày sinh là bắt buộc.")
                .LessThan(DateTime.Today).WithMessage("Ngày sinh phải là ngày trong quá khứ.");

            RuleFor(x => x.IdentityCardNumber)
                .Matches(@"^\d{6,12}$").When(x => !string.IsNullOrEmpty(x.IdentityCardNumber))
                .WithMessage("Số CMND phải từ 6 đến 12 chữ số.");

            RuleFor(x => x.Nationality)
                .NotEmpty().WithMessage("Quốc tịch là bắt buộc.")
                .Matches(@"^[a-zA-Z\sàáảãạâầấẩẫậèéẻẽẹêềếểễệìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵ\p{L}]+$")
                .WithMessage("Họ và tên chỉ được chứa chữ cái và khoảng trắng.");

            RuleFor(x => x.PlaceOfOrigin)
                .Matches(@"^[a-zA-Z0-9\s,àáảãạâầấẩẫậèéẻẽẹêềếểễệìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵ\p{L}]*$")
                .WithMessage("Nơi sinh chỉ được chứa chữ cái, số, khoảng trắng và dấu phẩy.")
                .When(x => !string.IsNullOrEmpty(x.PlaceOfOrigin));

            RuleFor(x => x.PlaceOfResidence)
                .MaximumLength(100).WithMessage("Nơi cư trú không được vượt quá 100 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.PlaceOfResidence));

        }
    }
}
