using RealEstateProjectSaleBusinessObject.BusinessObject;
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
        private readonly ICustomerServices _customerServices;
        private readonly IContractPaymentDetailServices _contractPaymentDetailServices;





        public DashboardService(IBookingServices bookingServices, IContractPaymentDetailServices contractDetailService,
           IPropertyServices propertyServices, ICustomerServices customerServices, IContractPaymentDetailServices contractPaymentDetailServices)
        {
            _bookingServices = bookingServices;
            _contractDetailService = contractDetailService;
            _propertyServices = propertyServices;
            _customerServices = customerServices;
            _contractPaymentDetailServices = contractPaymentDetailServices;
        }

        public int CalculateProperty()
        {
            int total = 0;
            var properties = _propertyServices.GetProperty();
          

            foreach ( var property in properties)
            {
                if (property.Status== "Đã bán")
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

        public double CalculateOutstandingAmount()
        {
            double total = 0;
            double pricecontractdetail = 0;
            double pricebooking = 0;
        


            var bookings = _bookingServices.GetBookings();
            foreach (var booking in bookings)
            {
                if (booking.DepositedPrice.HasValue && booking.Status== "Chưa thanh toán tiền giữ chỗ")
                {
                    pricebooking += booking.DepositedPrice.Value;
                }
            }

            var contractpaymentdetails = _contractPaymentDetailServices.GetAllContractPaymentDetail();
            foreach (var contractpaymentdetail in contractpaymentdetails)
            {
                if (contractpaymentdetail.Status == false)
                {
                    pricecontractdetail += contractpaymentdetail.PaidValue.Value ;

                    if (contractpaymentdetail.PaidValueLate.HasValue)
                    {
                        pricecontractdetail += contractpaymentdetail.PaidValueLate.Value;
                    }
                }
            }

             total = pricebooking + pricecontractdetail;
            return total;
        }




        public double CalculateTotalPrice()
        {
            double total = 0;
            double pricecontract = 0;
            double pricebooking = 0;
            double pricebookingcancel = 0;


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

            foreach (var booking in bookings)
            {
                if (booking.Status.Equals("Đã hủy"))
                {
                    pricebookingcancel += booking.DepositedPrice.Value;
                }
            }



            total = pricebooking + pricecontract - pricebookingcancel;
            return total;
        }

        public int CalculateCustomer()
        {
            int total = 0;
            var customers = _customerServices.GetAllCustomer();
            foreach(var customer in customers) {
                if(customer.Status == true)
                {
                    total += 1;
                }
            }
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
