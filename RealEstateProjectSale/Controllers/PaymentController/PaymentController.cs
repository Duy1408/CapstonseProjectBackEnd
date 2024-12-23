﻿using Microsoft.AspNetCore.Hosting.Server.Features;
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
using RealEstateProjectSaleBusinessObject.Enums;
using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
using Google;
using Stripe.BillingPortal;
using Microsoft.AspNetCore.Authorization;

namespace RealEstateProjectSale.Controllers.PaymentController
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentServices _paymentServices;
        private readonly IBookingServices _bookServices;
        private readonly ICustomerServices _customerServices;
        private readonly IDocumentTemplateService _documentService;
        private readonly IFileUploadToBlobService _fileService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;


        public PaymentController(IPaymentServices paymentServices, IBookingServices bookServices,
            IFileUploadToBlobService fileService, IDocumentTemplateService documentService, IMapper mapper,
            IConfiguration configuration, ICustomerServices customerServices)
        {
            _paymentServices = paymentServices;
            _bookServices = bookServices;
            _documentService = documentService;
            _fileService = fileService;
            _configuration = configuration;
            _mapper = mapper;
            _customerServices = customerServices;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Checkout Payment Deposited")]
        public async Task<IActionResult> CheckoutPayment([FromQuery] Guid bookingId)
        {
            try
            {
                var booking = _bookServices.GetBookingById(bookingId);

                var payment = new PaymentInformationModel
                {
                    PaymentID = Guid.NewGuid(),
                    CreatedTime = DateTime.Now,
                    BookingID = bookingId,
                    CustomerID = booking.CustomerID
                };

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

        [HttpPost("stripe-webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var webhookSecret = _configuration["Stripe:WebhookSecret"];
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    webhookSecret
                );

                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                    if (!paymentIntent.Metadata.TryGetValue("customCustomerID", out string customCustomerID))
                    {
                        Console.WriteLine("Custom Customer ID is missing in PaymentIntent metadata.");
                        return BadRequest(new { message = "Custom Customer ID is missing in PaymentIntent metadata." });
                    }
                    var customerIDCache = Guid.Parse(customCustomerID);

                    var model = _paymentServices.GetPaymentModelFromCache(customerIDCache);

                    var newPayment = new PaymentCreateDTO
                    {
                        PaymentID = model.PaymentID,
                        Amount = paymentIntent.Amount,
                        Content = paymentIntent.Description,
                        CreatedTime = model.CreatedTime,
                        PaymentTime = DateTime.Now,
                        Status = true,
                        BookingID = model.BookingID,
                        CustomerID = customerIDCache
                    };

                    var payment = _mapper.Map<Payment>(newPayment);
                    _paymentServices.AddNewPayment(payment);

                    var book = _bookServices.GetBookingById(newPayment.BookingID);
                    if (book != null)
                    {
                        book.DepositedTimed = newPayment.PaymentTime;
                        book.DepositedPrice = newPayment.Amount;
                        book.UpdatedTime = DateTime.Now;
                        book.Status = BookingStatus.DaDatCho.GetEnumDescription();
                        book.Note = newPayment.Content;
                        _bookServices.UpdateBooking(book);

                        var htmlContent = _bookServices.GenerateDocumentContent(book.BookingID);
                        var pdfBytes = _documentService.GeneratePdfFromTemplate(htmlContent);
                        string? blobUrl = null;
                        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
                        {
                            blobUrl = _fileService.UploadSingleFile(pdfStream, book.DocumentTemplate!.DocumentName, "bookingfile");
                        }

                        book.BookingFile = blobUrl;
                        _bookServices.UpdateBooking(book);

                    }

                    return Ok(new
                    {
                        message = "Hoàn thành thanh toán."
                    });

                }

                return BadRequest(new
                {
                    message = "Thanh toán thất bại."
                });
            }
            catch (StripeException e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin,Staff")]
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
                        message = "Thanh toán không tồn tại."
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

        [Authorize(Roles = "Admin,Staff")]
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
                message = "Thanh toán không tồn tại."
            });

        }

        [Authorize(Roles = "Admin,Staff")]
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
                        message = "Cập nhật thanh toán thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Thanh toán không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Payment")]
        public IActionResult DeletePayment(Guid id)
        {

            var payment = _paymentServices.GetPaymentByID(id);
            if (payment == null)
            {
                return NotFound(new
                {
                    message = "Thanh toán không tồn tại."
                });
            }

            _paymentServices.ChangeStatusPayment(payment);

            return Ok(new
            {
                message = "Xóa thanh toán thành công."
            });
        }


    }
}
