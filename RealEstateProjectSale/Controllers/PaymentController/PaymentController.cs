using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.Model;

namespace RealEstateProjectSale.Controllers.PaymentController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        private static string s_wasmClientURL = string.Empty;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> CheckoutPayment([FromBody] PaymentInformationModel payment, [FromServices] IServiceProvider sp)
        {
            var referer = Request.Headers.Referer;
            s_wasmClientURL = referer[0];

            // Build the URL to which the customer will be redirected after paying.
            var server = sp.GetRequiredService<IServer>();

            var serverAddressesFeature = server.Features.Get<IServerAddressesFeature>();

            string? thisApiUrl = null;

            if (serverAddressesFeature is not null)
            {
                thisApiUrl = serverAddressesFeature.Addresses.FirstOrDefault();
            }

            if (thisApiUrl is not null)
            {
                //var sessionId = await CheckOut(payment, thisApiUrl);

                var options = new SessionCreateOptions
                {
                    // Stripe calls the URLs below when certain checkout events happen such as success and failure.
                    SuccessUrl = $"{thisApiUrl}/api/Payment/success/" + "{CHECKOUT_SESSION_ID}", // Customer paid.
                    CancelUrl = s_wasmClientURL + "/failed",  // Checkout cancelled.
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                    {
                    new()
                    {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)payment.Amount, // Price is in USD cents.
                        Currency = "VND",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = payment.Content
                        },
                    },
                    Quantity = 1,
                },
            },
                    Mode = "payment" // One-time payment. Stripe supports recurring 'subscription' payments.
                };

                var service = new SessionService();
                var session = await service.CreateAsync(options);
                Response.Headers.Add("Location", session.Url);

                var pubKey = _configuration["Stripe:PubKey"];

                var paymentResponseModel = new PaymentResponseModel()
                {
                    SessionId = session.Id,
                    PubKey = pubKey
                };

                return Ok(paymentResponseModel);
            }
            else
            {
                return StatusCode(500);
            }
        }

        //[NonAction]
        //public async Task<string> CheckOut(PaymentInformationModel payment, string thisApiUrl)
        //{
        //    // Create a payment flow from the items in the cart.
        //    // Gets sent to Stripe API.
        //    var options = new SessionCreateOptions
        //    {
        //        // Stripe calls the URLs below when certain checkout events happen such as success and failure.
        //        SuccessUrl = $"{thisApiUrl}/api/Payment/success/" + "{CHECKOUT_SESSION_ID}", // Customer paid.
        //        CancelUrl = s_wasmClientURL + "/failed",  // Checkout cancelled.
        //        PaymentMethodTypes = new List<string> // Only card available in test mode?
        //    {
        //        "card"
        //    },
        //        LineItems = new List<SessionLineItemOptions>
        //    {
        //        new()
        //        {
        //            PriceData = new SessionLineItemPriceDataOptions
        //            {
        //                UnitAmount = (long)payment.Amount, // Price is in USD cents.
        //                Currency = "VND",
        //                ProductData = new SessionLineItemPriceDataProductDataOptions
        //                {
        //                    Name = payment.Content
        //                },
        //            },
        //            Quantity = 1,
        //        },
        //    },
        //        Mode = "payment" // One-time payment. Stripe supports recurring 'subscription' payments.
        //    };

        //    var service = new SessionService();
        //    var session = await service.CreateAsync(options);
        //    Response.Headers.Add("Location", session.Url);
        //    return session.Id;
        //}

        [HttpGet("success/{sessionId}")]
        // Automatic query parameter handling from ASP.NET.
        // Example URL: https://localhost:7051/checkout/success?sessionId=si_123123123123
        public ActionResult CheckoutSuccess(string sessionId)
        {
            var sessionService = new SessionService();
            var session = sessionService.Get(sessionId);

            // Here you can save order and customer details to your database.
            var total = session.AmountTotal.Value;
            //var customerEmail = session.CustomerDetails.Email;

            return Redirect(s_wasmClientURL + "success");
        }

    }
}
