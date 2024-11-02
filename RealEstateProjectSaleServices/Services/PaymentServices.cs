using Azure;
using Azure.Core;
using Microsoft.Extensions.Configuration;
using RealEstateProjectSaleBusinessObject.Model;
using RealEstateProjectSaleServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe.Checkout;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleRepository.IRepository;
using Stripe;

namespace RealEstateProjectSaleServices.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IPaymentRepo _paymentRepo;
        private readonly IBookingServices _bookService;
        private readonly IOpeningForSaleServices _openService;
        private readonly IProjectCategoryDetailServices _detailService;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        public PaymentServices(IPaymentRepo paymentRepo, IConfiguration configuration,
                    IMemoryCache memoryCache, IBookingServices bookService, IOpeningForSaleServices openService,
                    IProjectCategoryDetailServices detailService)
        {
            _paymentRepo = paymentRepo;
            _configuration = configuration;
            _memoryCache = memoryCache;
            _bookService = bookService;
            _openService = openService;
            _detailService = detailService;
        }

        public async Task<PaymentResponseModel> CreatePaymentUrl(PaymentInformationModel payment)
        {
            var book = _bookService.GetBookingById(payment.BookingID);
            var detail = _detailService.GetProjectCategoryDetailByID(book.ProjectCategoryDetailID);
            var openForSale = _openService.GetOpeningForSaleById(book.OpeningForSaleID);

            _memoryCache.Set($"Order_{payment.CustomerID}", payment, TimeSpan.FromMinutes(10));

            var customerOptions = new CustomerCreateOptions();
            var customerService = new CustomerService();
            var customer = customerService.Create(customerOptions);

            var ephemeralKeyOptions = new EphemeralKeyCreateOptions
            {
                Customer = customer.Id,
                StripeVersion = "2024-06-20",
            };

            var ephemeralKeyService = new EphemeralKeyService();
            var ephemeralKey = ephemeralKeyService.Create(ephemeralKeyOptions);

            var paymentIntentOptions = new PaymentIntentCreateOptions
            {
                Amount = (long)book.OpeningForSale.ReservationPrice,
                Currency = "VND",
                Customer = customer.Id,
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
                Description = $"Thanh toán giữ chỗ {detail.Project.ProjectName} {book.OpeningForSale.DecisionName}",
                Metadata = new Dictionary<string, string>
                {
                    { "customCustomerID", payment.CustomerID.ToString() } // Lưu customerID tùy chỉnh vào metadata
                }

            };
            var paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent = paymentIntentService.Create(paymentIntentOptions);

            var pubKey = _configuration["Stripe:PubKey"];
            return new PaymentResponseModel
            {
                PaymentIntent = paymentIntent.ClientSecret,
                EphemeralKey = ephemeralKey.Secret,
                Customer = customer.Id,
                PublishableKey = pubKey
            };

        }

        public PaymentInformationModel GetPaymentModelFromCache(Guid customerID)
        {
            return _memoryCache.Get<PaymentInformationModel>($"Order_{customerID}");
        }

        public List<Payment> GetAllPayment() => _paymentRepo.GetAllPayment();

        public Payment GetPaymentByID(Guid id) => _paymentRepo.GetPaymentByID(id);

        public void AddNewPayment(Payment payment) => _paymentRepo.AddNewPayment(payment);

        public void UpdatePayment(Payment payment) => _paymentRepo.UpdatePayment(payment);

        public bool ChangeStatusPayment(Payment payment) => _paymentRepo.ChangeStatusPayment(payment);

    }
}
