using RealEstateProjectSaleBusinessObject.Enums;
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
        private readonly IPropertyServices _propertyServices;





        public DashboardService(IBookingServices bookingServices, IContractPaymentDetailServices contractDetailService,
           IPropertyServices propertyServices)
        {
            _bookingServices = bookingServices;
            _contractDetailService = contractDetailService;
            _propertyServices = propertyServices;
        }

        public int CalculateProperty()
        {
            int total = 0;
            var properties = _propertyServices.GetProperty();
          

            foreach ( var property in properties)
            {
                if (property.Status=="DaBan")
                {
                    total++;
                }
            }
            return total ;
        }

        public int SumProperty()
        {
           
            var sumproperties = _propertyServices.GetProperty().Count();
            return sumproperties;
        }




        public double CalculateTotalPrice()
        {
            double total = 0;
            double pricecontract = 0;
            double pricebooking = 0;

            var orderdetails = _contractDetailService.GetAllContractPaymentDetail();
            foreach (var orderdetail in orderdetails)
            {
                if (orderdetail.Status == true && orderdetail.PaidValue.HasValue)
                {
                    pricecontract += orderdetail.PaidValue.Value;
                }
            }

            var bookings = _bookingServices.GetBookings();
            foreach (var booking in bookings)
            {
                if (booking.DepositedPrice.HasValue)
                {
                    pricebooking += booking.DepositedPrice.Value;
                }
            }

            total = pricebooking + pricecontract;
            return total;
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
