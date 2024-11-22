using FluentValidation;
using RealEstateProjectSale.Helpers;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.JsonConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSale.Validations.Request
{
    public class OpeningForSaleRequestDTOValidator : AbstractValidator<OpeningForSaleRequestDTO>
    {
        public OpeningForSaleRequestDTOValidator()
        {
            RuleFor(x => x.DecisionName)
                .NotEmpty().WithMessage("Tên quyết định là bắt buộc.")
                .Length(2, 50).WithMessage("Tên quyết định phải có độ dài từ 2 đến 50 ký tự.");

            RuleFor(x => x.SaleType)
                .NotEmpty().WithMessage("Loại hình bán hàng là bắt buộc.")
                .Must(value => value == "Online" || value == "Offline")
                .WithMessage("Loại hình bán hàng phải là 'Online' hoặc 'Offline'.");

            RuleFor(x => x.ReservationPrice)
                .NotNull().WithMessage("Giá giữ chỗ là bắt buộc.")
                .GreaterThanOrEqualTo(20000000).WithMessage("Giá giữ chỗ phải từ 20.000.000 đến 50.000.000.")
                .LessThanOrEqualTo(50000000).WithMessage("Giá giữ chỗ phải từ 20.000.000 đến 50.000.000.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Ngày bắt đầu là bắt buộc.")
                .Must(BeValidDateFormat).WithMessage("Ngày bắt đầu phải theo định dạng yyyy-MM-dd HH:mm:ss.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("Ngày kết thúc là bắt buộc.")
                .Must(BeValidDateFormat).WithMessage("Ngày kết thúc phải theo định dạng yyyy-MM-dd HH:mm:ss.");

            RuleFor(x => x.CheckinDate)
                .NotEmpty().WithMessage("Ngày checkin là bắt buộc.")
                .Must(BeValidDateFormat).WithMessage("Ngày checkin phải theo định dạng yyyy-MM-dd HH:mm:ss.");

            RuleFor(x => x)
                .Must(x => BeValidDateRange(x.StartDate, x.CheckinDate, x.EndDate))
                .WithMessage("Yêu cầu: Ngày bắt đầu < Ngày checkin < Ngày kết thúc.");

            RuleFor(x => x.ProjectCategoryDetailID)
                .NotEmpty().WithMessage("ProjectCategoryDetailID là bắt buộc.")
                .Must(id => id != Guid.Empty).WithMessage("ProjectCategoryDetailID phải là GUID hợp lệ.");
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
