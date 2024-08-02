using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.Model;
using RealEstateProjectSaleServices.IServices;
using System.Data;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.ViewModels;

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
        [Route("CheckoutPaymentDeposited")]
        public async Task<IActionResult> CheckoutPayment([FromBody] PaymentInformationModel payment)
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
        // Example URL: https://localhost:7051/checkout/success/{sessionId}
        public IActionResult CheckoutSuccess(string sessionId)
        {
            var session = _paymentServices.CheckoutSuccess(sessionId);

            var userId = Guid.Parse(HttpContext.Request.Query["UserID"]);
            var model = _paymentServices.GetPaymentModelFromCache(userId);

            // Chỗ lưu xuống db
            var newPayment = new PaymentCreateDTO
            {
                PaymentID = model.PaymentID,
                Amount = session.AmountTotal.Value,
                Content = model.Content,
                CreatedTime = model.CreatedTime,
                PaymentTime = DateTime.Now,
                Status = true,
                PaymentTypeID = model.PaymentTypeID,
                BookingID = model.BookingID,
                ContractPaymentDetailID = model.ContractPaymentDetailID

            };
            //var total = session.AmountTotal.Value;
            //var customerEmail = session.CustomerDetails.Email;



            //return Redirect(s_wasmClientURL + "success");
            return Redirect("https://localhost:7022/swagger/index.html/success");
        }



    }
}
