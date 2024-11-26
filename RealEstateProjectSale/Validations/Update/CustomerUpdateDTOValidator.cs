using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;

namespace RealEstateProjectSale.Validations.Update
{
    public class CustomerUpdateDTOValidator : AbstractValidator<CustomerUpdateDTO>
    {
        public CustomerUpdateDTOValidator()
        {
            RuleFor(x => x.FullName)
                .Matches(@"^[a-zA-Z\s]*$").WithMessage("Họ tên chỉ được chứa chữ cái và khoảng trắng.")
                .When(x => !string.IsNullOrEmpty(x.FullName));

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Today).WithMessage("Ngày sinh phải là một ngày trong quá khứ.")
                .When(x => x.DateOfBirth.HasValue);

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\d{7,11}$").WithMessage("Số điện thoại phải từ 7 đến 11 chữ số.")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

            RuleFor(x => x.IdentityCardNumber)
                .Matches(@"^\d{6,12}$").When(x => !string.IsNullOrEmpty(x.IdentityCardNumber))
                .WithMessage("Số CMND/CCCD phải từ 6 đến 12 chữ số.");

            RuleFor(x => x.Nationality)
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Họ và tên chỉ được chứa chữ cái và khoảng trắng.")
                .When(x => !string.IsNullOrEmpty(x.Nationality));

            RuleFor(x => x.PlaceofOrigin)
                .Matches(@"^[a-zA-Z\s]*$").WithMessage("Nơi sinh chỉ được chứa chữ cái và khoảng trắng.")
                .When(x => !string.IsNullOrEmpty(x.PlaceofOrigin));

            RuleFor(x => x.PlaceOfResidence)
                .Matches(@"^[a-zA-Z\s]*$").WithMessage("Nơi cư trú chỉ được chứa chữ cái và khoảng trắng.")
                .When(x => !string.IsNullOrEmpty(x.PlaceOfResidence));

            RuleFor(x => x.DateOfExpiry)
                .Matches(@"^\d{2}-\d{2}-\d{4}$").When(x => !string.IsNullOrEmpty(x.DateOfExpiry))
                .WithMessage("Ngày hết hạn phải theo định dạng MM-dd-yyyy.");

            RuleFor(x => x.Taxcode)
                .Matches(@"^\d{10}$").When(x => !string.IsNullOrEmpty(x.Taxcode))
                .WithMessage("Mã số thuế phải có 10 chữ số.");

            RuleFor(x => x.BankName)
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.BankNumber))
                .WithMessage("Tên ngân hàng là bắt buộc khi có số tài khoản ngân hàng.");

            RuleFor(x => x.BankNumber)
                .Matches(@"^\d{6,20}$").When(x => !string.IsNullOrEmpty(x.BankNumber))
                .WithMessage("Số tài khoản ngân hàng phải từ 6 đến 20 chữ số.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Địa chỉ là bắt buộc.")
                .When(x => !string.IsNullOrEmpty(x.Address));

            RuleFor(x => x.Status)
                .NotNull().WithMessage("Trạng thái không được để trống.")
                .When(x => x.Status.HasValue);




        }
    }
}
