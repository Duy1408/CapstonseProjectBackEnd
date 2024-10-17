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
using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using Stripe;

namespace RealEstateProjectSale.Controllers.PaymentController
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentServices _paymentServices;
        private readonly IMapper _mapper;


        public PaymentController(IPaymentServices paymentServices, IMapper mapper)
        {
            _paymentServices = paymentServices;
            _mapper = mapper;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Checkout Payment Deposited")]
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
        public IActionResult CheckoutSuccess(string sessionId, [FromQuery] Guid customerID)
        {
            var session = _paymentServices.CheckoutSuccess(sessionId);

            var customerIDCache = Guid.Parse(HttpContext.Request.Query["customerID"]);
            var model = _paymentServices.GetPaymentModelFromCache(customerIDCache);

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
                CustomerID = customerIDCache
            };

            var payment = _mapper.Map<Payment>(newPayment);
            _paymentServices.AddNewPayment(payment);
            //var total = session.AmountTotal.Value;
            //var customerEmail = session.CustomerDetails.Email;



            //return Redirect(s_wasmClientURL + "success");
            return Redirect("https://localhost:7022/swagger/index.html/success");
        }



    }
}
