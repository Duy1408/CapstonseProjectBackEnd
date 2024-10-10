using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.PaymentProcessDetailController
{
    [Route("api/payment-process-details")]
    [ApiController]
    public class PaymentProcessDetailsController : ControllerBase
    {
        private readonly IPaymentProcessDetailServices _detailService;
        private readonly IMapper _mapper;

        public PaymentProcessDetailsController(IPaymentProcessDetailServices detailService, IMapper mapper)
        {
            _detailService = detailService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get PaymentProcessDetail by ID")]
        public IActionResult GetAllPaymentProcessDetail()
        {
            try
            {
                if (_detailService.GetPaymentProcessDetail() == null)
                {
                    return NotFound(new
                    {
                        message = "PaymentProcessDetail not found."
                    });
                }
                var details = _detailService.GetPaymentProcessDetail();
                var response = _mapper.Map<List<PaymentProcessDetailVM>>(details);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get PaymentProcessDetail By ID")]
        public IActionResult GetPaymentProcessDetailByID(Guid id)
        {
            var detail = _detailService.GetPaymentProcessDetailById(id);

            if (detail != null)
            {
                var responese = _mapper.Map<PaymentProcessDetailVM>(detail);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "PaymentProcessDetail not found."
            });

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new PaymentProcessDetail")]
        public IActionResult AddNew(PaymentProcessDetailCreateDTO detail)
        {
            try
            {
                var newDetail = new PaymentProcessDetailCreateDTO
                {
                    PaymentProcessDetailID = Guid.NewGuid(),
                    DetailName = detail.DetailName,
                    PeriodType = detail.PeriodType,
                    Period = detail.Period,
                    PaymentRate = detail.PaymentRate,
                    PaymentType = detail.PaymentType,
                    Amount = detail.Amount,
                    Note = detail.Note,
                    PaymentProcessID = detail.PaymentProcessID,
                };

                var _detail = _mapper.Map<PaymentProcessDetail>(newDetail);
                _detailService.AddNew(_detail);

                return Ok(new
                {
                    message = "Create PaymentProcessDetail Successfully"
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update PaymentProcessDetail by ID")]
        public IActionResult UpdatePaymentProcessDetail([FromForm] PaymentProcessDetailUpdateDTO detail, Guid id)
        {
            try
            {
                var existingDetail = _detailService.GetPaymentProcessDetailById(id);
                if (existingDetail != null)
                {

                    if (!string.IsNullOrEmpty(detail.DetailName))
                    {
                        existingDetail.DetailName = detail.DetailName;
                    }
                    if (!string.IsNullOrEmpty(detail.PeriodType))
                    {
                        existingDetail.PeriodType = detail.PeriodType;
                    }
                    if (!string.IsNullOrEmpty(detail.Period))
                    {
                        existingDetail.Period = detail.Period;
                    }
                    if (detail.PaymentRate.HasValue)
                    {
                        existingDetail.PaymentRate = detail.PaymentRate.Value;
                    }
                    if (!string.IsNullOrEmpty(detail.PaymentType))
                    {
                        existingDetail.PaymentType = detail.PaymentType;
                    }
                    if (detail.Amount.HasValue)
                    {
                        existingDetail.Amount = detail.Amount.Value;
                    }
                    if (!string.IsNullOrEmpty(detail.Note))
                    {
                        existingDetail.Note = detail.Note;
                    }

                    _detailService.UpdatePaymentProcessDetail(existingDetail);

                    return Ok(new
                    {
                        message = "Update PaymentProcessDetail Successfully"
                    });

                }

                return NotFound(new
                {
                    message = "PaymentProcessDetail not found."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete PaymentProcessDetail by ID")]
        public IActionResult DeletePaymentProcessDetailByID(Guid id)
        {
            try
            {
                var detail = _detailService.GetPaymentProcessDetailById(id);
                if (detail != null)
                {
                    _detailService.DeletePaymentProcessDetailByID(id);
                    return Ok(new
                    {
                        message = "Delete PaymentProcessDetail Successfully"
                    });
                }

                return NotFound(new
                {
                    message = "PaymentProcessDetail not found."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
