using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.Validation.Create
{
    public class AccountCreateDTOValidator : AbstractValidator<AccountCreateDTO>
    {
        public AccountCreateDTOValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid Email format.");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(x => x.RoleID)
            .NotEmpty().WithMessage("RoleID is required.");
        }
    }
}
