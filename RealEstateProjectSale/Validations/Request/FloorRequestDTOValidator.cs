using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Request;

namespace RealEstateProjectSale.Validations.Request
{
    public class FloorRequestDTOValidator : AbstractValidator<FloorRequestDTO>
    {
        public FloorRequestDTOValidator()
        {
            RuleFor(x => x.NumFloor)
              .NotNull().WithMessage("Số tầng không được để trống.")
              .GreaterThan(0).WithMessage("Số tầng phải lớn hơn 0.")
              .LessThan(100).WithMessage("Số tầng phải nhỏ hơn 100.");

        }
    }
}
