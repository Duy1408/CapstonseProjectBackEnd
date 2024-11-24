using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.Enums;

namespace RealEstateProjectSale.Validations.Update
{
    public class BookingUpdateDTOValidator : AbstractValidator<BookingUpdateDTO>
    {
        public BookingUpdateDTOValidator()
        {
            RuleFor(x => x.Note)
                .MaximumLength(500).WithMessage("Ghi chú không được vượt quá 500 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Note));

            RuleFor(x => x.Status)
                .Must(status =>
                {
                    if (string.IsNullOrEmpty(status)) return true;
                    return int.TryParse(status, out var value) && Enum.IsDefined(typeof(BookingStatus), value);
                })
                .WithMessage("Trạng thái không hợp lệ. Vui lòng nhập số tương ứng với enum BookingStatus.");


        }
    }
}
