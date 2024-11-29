using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Request;

namespace RealEstateProjectSale.Validations.Request
{
    public class UnitTypeRequestDTOValidator : AbstractValidator<UnitTypeRequestDTO>
    {
        public UnitTypeRequestDTOValidator()
        {
            RuleFor(x => x.BathRoom)
               .NotEmpty().WithMessage(" Nhập số phòng tắm ")
               .InclusiveBetween(1, 5).WithMessage("Số phòng tắm phải từ 1 đến 5.");

            RuleFor(x => x.NetFloorArea)
               .NotEmpty().WithMessage("Nhập diện tích sàn.")
               .Must(x => x > 10).WithMessage("Diện tích sàn phải lớn hơn 10m².")
               .GreaterThan(10).WithMessage("Diện tích sàn phải lớn hơn 10.")
               .LessThanOrEqualTo(400).WithMessage("Diện tích sàn không được vượt quá 400m².");

            RuleFor(x => x.GrossFloorArea)
               .NotEmpty().WithMessage("Nhập tổng diện tích.")
               .Must(x => x > 10).WithMessage(" Tổng diện tích sàn phải lớn hơn 10m².")
               .GreaterThan(10).WithMessage("Tổng diện tích phải lớn hơn 10m².")
               .LessThanOrEqualTo(400).WithMessage("Tổng diện tích không được vượt quá 400m².")
               .Must((model, grossFloorArea) => grossFloorArea > model.NetFloorArea)
               .WithMessage("Tổng diện tích phải lớn hơn diện tích sàn.");

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
              .NotEmpty().WithMessage(" Nhập số tầng ")
              .InclusiveBetween(1, 4).WithMessage("Số tầng phải từ 1 đến 4.");

            RuleFor(x => x.Basement)
             .NotEmpty().WithMessage(" Nhập số tầng hầm ")
             .InclusiveBetween(1, 3).WithMessage("Số tầng hầm phải từ 1 đến 3.");
        }
    }
}
