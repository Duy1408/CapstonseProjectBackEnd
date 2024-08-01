using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.Model;
using RealEstateProjectSaleServices.IServices;

namespace RealEstateProjectSale.Controllers.PaymentController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentServices _paymentServices;


        public PaymentController(IPaymentServices paymentServices)
        {
            _paymentServices = paymentServices;
        }

        [HttpPost]
        public async Task<ActionResult> CheckoutPayment([FromBody] PaymentInformationModel payment)
        {
            try
            {
                var paymentResponseModel = await _paymentServices.CreatePaymentUrl(payment);

                if (paymentResponseModel != null)
                {
                    return Ok(paymentResponseModel);
                }

                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("success/{sessionId}")]
        // Automatic query parameter handling from ASP.NET.
        // Example URL: https://localhost:7051/checkout/success?sessionId=si_123123123123
        public ActionResult CheckoutSuccess(string sessionId)
        {
            var session = _paymentServices.CheckoutSuccess(sessionId);

            // Chỗ lưu xuống db
            var total = session.AmountTotal.Value;
            //var customerEmail = session.CustomerDetails.Email;

            //return Redirect(s_wasmClientURL + "success");
            return Redirect("https://localhost:7022/swagger/index.html/success");
        }



    }
}
