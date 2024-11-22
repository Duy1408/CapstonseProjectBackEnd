using FluentValidation;
using RealEstateProjectSale.Helpers;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Validations.Update
{
    public class OpeningForSaleUpdateDTOValidator : AbstractValidator<OpeningForSaleUpdateDTO>
    {
        public OpeningForSaleUpdateDTOValidator()
        {
            RuleFor(x => x.DecisionName)
                .Length(2, 50).WithMessage("Tên quyết định phải có độ dài từ 2 đến 50 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.DecisionName));

            RuleFor(x => x.SaleType)
                .Must(value => value == "Online" || value == "Offline")
                .WithMessage("Loại hình bán hàng phải là 'Online' hoặc 'Offline'.")
                .When(x => !string.IsNullOrEmpty(x.SaleType));

            RuleFor(x => x.ReservationPrice)
                .GreaterThanOrEqualTo(20000000).WithMessage("Giá giữ chỗ phải từ 20.000.000 đến 50.000.000.")
                .LessThanOrEqualTo(50000000).WithMessage("Giá giữ chỗ phải từ 20.000.000 đến 50.000.000.")
                .When(x => x.ReservationPrice.HasValue);

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.StartDate)
                .Must(BeValidDateFormat).WithMessage("Ngày bắt đầu phải theo định dạng yyyy-MM-dd HH:mm:ss.")
                .When(x => !string.IsNullOrEmpty(x.StartDate));

            RuleFor(x => x.EndDate)
                .Must(BeValidDateFormat).WithMessage("Ngày kết thúc phải theo định dạng yyyy-MM-dd HH:mm:ss.")
                .When(x => !string.IsNullOrEmpty(x.EndDate));

            RuleFor(x => x.CheckinDate)
                .Must(BeValidDateFormat).WithMessage("Ngày checkin phải theo định dạng yyyy-MM-dd HH:mm:ss.")
                .When(x => !string.IsNullOrEmpty(x.CheckinDate));

            RuleFor(x => x)
                .Must(x => BeValidDateRange(x.StartDate, x.CheckinDate, x.EndDate))
                .WithMessage("Yêu cầu: Ngày bắt đầu < Ngày checkin < Ngày kết thúc.")
                .When(x => !string.IsNullOrEmpty(x.StartDate) && !string.IsNullOrEmpty(x.CheckinDate) && !string.IsNullOrEmpty(x.EndDate));

            RuleFor(x => x.ProjectCategoryDetailID)
                .Must(id => id != Guid.Empty).WithMessage("ProjectCategoryDetailID phải là GUID hợp lệ.")
                .When(x => x.ProjectCategoryDetailID.HasValue);
        }

        private bool BeValidDateFormat(string date)
        {
            try
            {
                DateTimeHelper.ConvertToDateTime(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool BeValidDateRange(string startDate, string checkinDate, string endDate)
        {
            try
            {
                var start = DateTimeHelper.ConvertToDateTime(startDate);
                var checkin = DateTimeHelper.ConvertToDateTime(checkinDate);
                var end = DateTimeHelper.ConvertToDateTime(endDate);

                return start < checkin && checkin < end;
            }
            catch
            {
                return false;
            }
        }

    }
}
