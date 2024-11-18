using RealEstateProjectSaleServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IBookingServices _bookingServices;
        private readonly IContractPaymentDetailServices _contractDetailService;


        public DashboardService(IBookingServices bookingServices, IContractPaymentDetailServices contractDetailService)
        {
            _bookingServices = bookingServices;
            _contractDetailService = contractDetailService;
        }
        public List<object> GetMonthlyTotalPrices()
        {
            int currentYear = DateTime.Now.Year;
            List<object> monthlyTotalPrices = new List<object>();
            for (int month = 1; month <= 12; month++)
            {
                double? totalPriceBookingOfMonth = _bookingServices.GetBookings()
              .Where(book => book.DepositedTimed?.Year == currentYear
              && book.DepositedTimed?.Month == month)
              .Sum(book => book.DepositedPrice);

                double? totalPaidValueOfMonth = _contractDetailService.GetAllContractPaymentDetail()
        .Where(contractdetail => contractdetail.Period?.Year == currentYear
        && contractdetail.Period?.Month == month && contractdetail.Status == true)
        .Sum(contractdetail => contractdetail.PaidValue);

                double? totalPaidValueLateOfMonth = _contractDetailService.GetAllContractPaymentDetail()
           .Where(contractdetail => contractdetail.Period?.Year == currentYear
          && contractdetail.Period?.Month == month && contractdetail.Status == true)
              .Sum(contractdetail => contractdetail.PaidValueLate);

                var monthlyTotalPrice = new
                {
                    Month = month,
                    TotalPrice = totalPriceBookingOfMonth + totalPaidValueOfMonth + totalPaidValueLateOfMonth
                };

                monthlyTotalPrices.Add(monthlyTotalPrice);

            }
            return monthlyTotalPrices;


        }
    }
}
