﻿using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;

namespace RealEstateProjectSale.Validations.Update
{
    public class StaffUpdateDTOValidator : AbstractValidator<StaffUpdateDTO>
    {
        public StaffUpdateDTOValidator()
        {
            RuleFor(x => x.Name)
                .Matches(@"^[\p{L}\s]+$").WithMessage("Họ và tên chỉ được chứa chữ cái và khoảng trắng.")
                .When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.PersonalEmail)
                .EmailAddress().WithMessage("Email cá nhân không hợp lệ.")
                .When(x => !string.IsNullOrEmpty(x.PersonalEmail));

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Today).WithMessage("Ngày sinh phải là ngày trong quá khứ.")
                .When(x => x.DateOfBirth.HasValue);

            RuleFor(x => x.IdentityCardNumber)
                .Matches(@"^\d{6,12}$").When(x => !string.IsNullOrEmpty(x.IdentityCardNumber))
                .WithMessage("Số CMND/CCCD phải từ 6 đến 12 chữ số.");

            RuleFor(x => x.Nationality)
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Họ và tên chỉ được chứa chữ cái và khoảng trắng.")
                .When(x => !string.IsNullOrEmpty(x.Nationality));

            RuleFor(x => x.PlaceOfOrigin)
                .Matches(@"^[a-zA-Z\s]*$").WithMessage("Nơi sinh chỉ được chứa chữ cái và khoảng trắng.")
                .When(x => !string.IsNullOrEmpty(x.PlaceOfOrigin));

            RuleFor(x => x.PlaceOfResidence)
                .Matches(@"^[a-zA-Z\s]*$").WithMessage("Nơi cư trú chỉ được chứa chữ cái và khoảng trắng.")
                .When(x => !string.IsNullOrEmpty(x.PlaceOfResidence));

            RuleFor(x => x.Status)
                .Must(status => status == null || status == true || status == false).WithMessage("Trạng thái không hợp lệ nếu có.");

        }
    }
}
