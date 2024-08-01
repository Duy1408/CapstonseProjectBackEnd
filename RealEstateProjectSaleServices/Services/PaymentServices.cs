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

namespace RealEstateProjectSaleServices.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IConfiguration _configuration;
        public PaymentServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<PaymentResponseModel> CreatePaymentUrl(PaymentInformationModel payment)
        {
            string s_wasmClientURL = "https://localhost:7022/swagger/index.html";
            var thisApiUrl = _configuration["PaymentApiUrl:ApiUrl"];

            var options = new SessionCreateOptions
            {
                // Stripe calls the URLs below when certain checkout events happen such as success and failure.
                SuccessUrl = $"{thisApiUrl}/api/Payment/success/" + "{CHECKOUT_SESSION_ID}", // Customer paid.
                CancelUrl = s_wasmClientURL + "/failed",  // Checkout cancelled.
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                    { new() {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)payment.Amount,
                            Currency = "VND",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = payment.Content
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
            var session = sessionService.Get(sessionId);

            return session;

        }



    }
}
