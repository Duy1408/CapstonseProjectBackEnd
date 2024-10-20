﻿using Azure;
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

namespace RealEstateProjectSaleServices.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IPaymentRepo _paymentRepo;
        private readonly IBookingServices _bookService;
        private readonly IOpeningForSaleServices _openService;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        public PaymentServices(IPaymentRepo paymentRepo, IConfiguration configuration,
                    IMemoryCache memoryCache, IBookingServices bookService, IOpeningForSaleServices openService)
        {
            _paymentRepo = paymentRepo;
            _configuration = configuration;
            _memoryCache = memoryCache;
            _bookService = bookService;
            _openService = openService;
        }

        public async Task<PaymentResponseModel> CreatePaymentUrl(PaymentInformationModel payment)
        {
            var book = _bookService.GetBookingById(payment.BookingID);
            var openForSale = _openService.GetOpeningForSaleById(book.OpeningForSaleID);

            _memoryCache.Set($"Order_{payment.CustomerID}", payment, TimeSpan.FromMinutes(10));

            string s_wasmClientURL = "https://realestateproject-bdhcgphcfsf6b4g2.canadacentral-01.azurewebsites.net/index.html";
            var thisApiUrl = _configuration["PaymentApiUrl:ApiUrl"];

            var firstImage = book.Project.Image
                                        .Split(',', StringSplitOptions.RemoveEmptyEntries)  // Tách URL bằng dấu phẩy
                                        .Select(image => image.Trim())  // Loại bỏ khoảng trắng thừa
                                        .FirstOrDefault();

            var options = new SessionCreateOptions
            {
                // Stripe calls the URLs below when certain checkout events happen such as success and failure.
                SuccessUrl = $"{thisApiUrl}/api/payments/success/" + "{CHECKOUT_SESSION_ID}" + "?customerID=" + payment.CustomerID, // Customer paid.
                CancelUrl = s_wasmClientURL + "/failed",  // Checkout cancelled.
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                    { new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = (long)book.OpeningForSale.ReservationPrice,
                                Currency = "VND",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = "Thanh toán giữ chỗ " + book.Project.ProjectName + " " + book.OpeningForSale.DecisionName,
                                    Images = new List<string> {firstImage }
                                },
                            },
                            Quantity = 1,
                        },
                    },
                Mode = "payment"
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            var pubKey = _configuration["Stripe:PubKey"];

            var paymentResponseModel = new PaymentResponseModel()
            {
                SessionId = session.Id,
                PubKey = pubKey,
                SessionUrl = session.Url
            };

            return paymentResponseModel;

        }

        public Session CheckoutSuccess(string sessionId)
        {
            var sessionService = new SessionService();
            var session = sessionService.Get(sessionId, new SessionGetOptions
            {
                Expand = new List<string> { "line_items" }
            });

            return session;

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
