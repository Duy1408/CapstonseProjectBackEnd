using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Validations.Update
{
    public class UnitTypeUpdateDTOValidator : AbstractValidator<UnitTypeUpdateDTO>
    {
        public UnitTypeUpdateDTOValidator()
        {

            RuleFor(x => x.BathRoom)
              .NotEmpty().WithMessage(" Nhập số phòng tắm ")
              .InclusiveBetween(1, 5).WithMessage("Số phòng tắm phải từ 1 đến 5.");
              

            RuleFor(x => x.NetFloorArea)
               .Must(x => x > 10).WithMessage("Diện tích sàn phải lớn hơn 10m².")
               .LessThanOrEqualTo(400).WithMessage("Diện tích sàn không được vượt quá 400m².")
               .When(x => x.NetFloorArea.HasValue);

            RuleFor(x => x.GrossFloorArea)
               .Must(x => x > 10).WithMessage(" Tổng diện tích phải lớn hơn 10m².")
               .LessThanOrEqualTo(400).WithMessage("Tổng diện tích không được vượt quá 400m².")
               .Must((model, grossFloorArea) => grossFloorArea > model.NetFloorArea)
               .WithMessage("Tổng diện tích phải lớn hơn diện tích sàn.")
               .When(x => x.GrossFloorArea.HasValue);

            RuleFor(x => x.BedRoom)
              .NotEmpty().WithMessage(" Nhập số phòng ngủ ")
              .InclusiveBetween(1, 5).WithMessage("Số phòng ngủ phải từ 1 đến 5.");


            RuleFor(x => x.KitchenRoom)
              .NotEmpty().WithMessage(" Nhập số nhà bếp ")
              .InclusiveBetween(1, 3).WithMessage("Số nhà bếp phải từ 1 đến 3.");


            RuleFor(x => x.LivingRoom)
              .NotEmpty().WithMessage(" Nhập số phòng khách ")
              .InclusiveBetween(1, 3).WithMessage("Số phòng khách phải từ 1 đến 3.");
             

            RuleFor(x => x.NumberFloor)
              .InclusiveBetween(1, 4).WithMessage("Số tầng phải từ 1 đến 4.")
              .When(x => x.NumberFloor.HasValue);

            RuleFor(x => x.Basement)
             .InclusiveBetween(1, 3).WithMessage("Số tầng hầm phải từ 1 đến 3.")
             .When(x => x.Basement.HasValue);

            RuleFor(x => x.Status)
               .Must(status => status == null || status == true || status == false).WithMessage("Trạng thái không hợp lệ.");
        }
    }
}
