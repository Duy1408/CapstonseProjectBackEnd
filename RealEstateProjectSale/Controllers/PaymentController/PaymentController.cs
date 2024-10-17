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
using RealEstateProjectSaleBusinessObject.DTO.Update;
using Stripe.FinancialConnections;
using RealEstateProjectSaleServices.Services;

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



            return Ok(new
            {
                message = "Payment completed successfully."
            });
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All Payment")]
        public IActionResult GetAllPayment()
        {
            try
            {
                if (_paymentServices.GetAllPayment() == null)
                {
                    return NotFound(new
                    {
                        message = "Payment not found."
                    });
                }
                var payments = _paymentServices.GetAllPayment();
                var response = _mapper.Map<List<PaymentVM>>(payments);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Payment By ID")]
        public IActionResult GetPaymentByID(Guid id)
        {
            var payment = _paymentServices.GetPaymentByID(id);

            if (payment != null)
            {
                var responese = _mapper.Map<PaymentVM>(payment);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Payment not found."
            });

        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Payment By ID")]
        public IActionResult UpdatePayment([FromForm] PaymentUpdateDTO payment, Guid id)
        {
            try
            {
                var existingPayment = _paymentServices.GetPaymentByID(id);
                if (existingPayment != null)
                {
                    if (payment.Amount.HasValue)
                    {
                        existingPayment.Amount = payment.Amount.Value;
                    }
                    if (!string.IsNullOrEmpty(payment.Content))
                    {
                        existingPayment.Content = payment.Content;
                    }
                    if (payment.Status.HasValue)
                    {
                        existingPayment.Status = payment.Status.Value;
                    }
                    if (payment.PaymentTypeID.HasValue)
                    {
                        existingPayment.PaymentTypeID = payment.PaymentTypeID.Value;
                    }
                    if (payment.BookingID.HasValue)
                    {
                        existingPayment.BookingID = payment.BookingID.Value;
                    }
                    if (payment.CustomerID.HasValue)
                    {
                        existingPayment.CustomerID = payment.CustomerID.Value;
                    }

                    _paymentServices.UpdatePayment(existingPayment);

                    return Ok(new
                    {
                        message = "Update Payment Successfully"
                    });

                }

                return NotFound(new
                {
                    message = "Payment not found."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Payment")]
        public IActionResult DeletePayment(Guid id)
        {

            var payment = _paymentServices.GetPaymentByID(id);
            if (payment == null)
            {
                return NotFound(new
                {
                    message = "Payment not found."
                });
            }

            _paymentServices.ChangeStatusPayment(payment);

            return Ok(new
            {
                message = "Delete Payment Successfully"
            });
        }


    }
}
