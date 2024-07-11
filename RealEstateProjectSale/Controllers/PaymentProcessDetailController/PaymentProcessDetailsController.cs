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

namespace RealEstateProjectSale.Controllers.PaymentProcessDetailController
{
    [Route("api/[controller]")]
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
        [Route("GetAllPaymentProcessDetail")]
        public IActionResult GetAllPaymentProcessDetail()
        {
            try
            {
                if (_detailService.GetPaymentProcessDetail() == null)
                {
                    return NotFound();
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

        [HttpGet("GetPaymentProcessDetailByID/{id}")]
        public IActionResult GetPaymentProcessDetailByID(Guid id)
        {
            var detail = _detailService.GetPaymentProcessDetailById(id);

            if (detail != null)
            {
                var responese = _mapper.Map<PaymentProcessDetailVM>(detail);

                return Ok(responese);
            }

            return NotFound();

        }

        [HttpPost]
        [Route("AddNewPaymentProcessDetail")]
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

                return Ok("Create PaymentProcessDetail Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdatePaymentProcessDetail/{id}")]
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

                    return Ok("Update PaymentProcessDetail Successfully");

                }

                return NotFound("PaymentProcessDetail not found.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeletePaymentProcessDetailByID/{id}")]
        public IActionResult DeletePaymentProcessDetailByID(Guid id)
        {
            try
            {
                var detail = _detailService.GetPaymentProcessDetailById(id);
                if (detail != null)
                {
                    _detailService.DeletePaymentProcessDetailByID(id);
                    return Ok("Deleted PaymentProcessDetail Successfully");
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
