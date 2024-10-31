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
                .NotEmpty().WithMessage("Decision Name is required.")
                .Length(2, 50).WithMessage("Decision Name must be between 2 and 50 characters.");

            RuleFor(x => x.SaleType)
                .NotEmpty().WithMessage("Sale Type is required.")
                .Must(value => value == "Online" || value == "Offline")
                .WithMessage("Sale Type must be either 'Online' or 'Offline'.");

            RuleFor(x => x.ReservationPrice)
                .NotNull().WithMessage("Reservation Price is required.")
                .GreaterThanOrEqualTo(20000000).WithMessage("Reservation Price must be between 20.000.000 and 50.000.000.")
                .LessThanOrEqualTo(50000000).WithMessage("Reservation Price must be between 20.000.000 and 50.000.000.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => x.Description != null);

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("StartDate is required.")
                .Must(BeValidDateFormat).WithMessage("StartDate must follow the format yyyy-MM-dd HH:mm:ss.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("EndDate is required.")
                .Must(BeValidDateFormat).WithMessage("EndDate must follow the format yyyy-MM-dd HH:mm:ss.");

            RuleFor(x => x.CheckinDate)
                .NotEmpty().WithMessage("CheckinDate is required.")
                .Must(BeValidDateFormat).WithMessage("CheckinDate must follow the format yyyy-MM-dd HH:mm:ss.");

            RuleFor(x => x)
                .Must(x => BeValidDateRange(x.StartDate, x.CheckinDate, x.EndDate))
                .WithMessage("Request: StartDate < CheckinDate < EndDate.");

            RuleFor(x => x.ProjectID)
                .NotEmpty().WithMessage("Project ID is required.")
                .Must(id => id != Guid.Empty).WithMessage("Project ID must be a valid GUID.");
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
