using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Validations.Update
{
    public class FloorUpdateDTOValidator : AbstractValidator<FloorUpdateDTO>
    {
        public FloorUpdateDTOValidator()
        {
            RuleFor(x => x.NumFloor)
                .Must(numFloor => numFloor == null || numFloor > 0).WithMessage("Số tầng phải lớn hơn 0 nếu có.")
                .LessThan(100).WithMessage("Số tầng phải nhỏ hơn 100.");

            RuleFor(x => x.Status)
                .Must(status => status == null || status == true || status == false).WithMessage("Trạng thái không hợp lệ nếu có.");


        }
    }
}
