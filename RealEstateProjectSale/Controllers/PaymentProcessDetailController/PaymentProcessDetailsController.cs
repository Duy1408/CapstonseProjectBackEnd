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
        [SwaggerOperation(Summary = "Get All PaymentProcessDetail")]
        public IActionResult GetAllPaymentProcessDetail()
        {
            try
            {
                if (_detailService.GetPaymentProcessDetail() == null)
                {
                    return NotFound(new
                    {
                        message = "Chi tiết đợt thanh toán không tồn tại."
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
                message = "Chi tiết đợt thanh toán không tồn tại."
            });

        }

        [HttpGet("paymentProcess/{pmtId}")]
        [SwaggerOperation(Summary = "Get PaymentProcessDetail By PaymentProcessID")]
        public IActionResult GetPaymentProcessDetailByPaymentProcessID(Guid pmtId)
        {
            var detail = _detailService.GetPaymentProcessDetailByPaymentProcessID(pmtId);

            if (detail != null)
            {
                var responese = _mapper.Map<List<PaymentProcessDetailVM>>(detail);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Chi tiết đợt thanh toán không tồn tại."
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
                    PaymentStage = detail.PaymentStage,
                    Percentage = detail.Percentage,
                    Amount = detail.Amount,
                    PaymentProcessID = detail.PaymentProcessID,
                };

                var _detail = _mapper.Map<PaymentProcessDetail>(newDetail);
                _detailService.AddNew(_detail);

                return Ok(new
                {
                    message = "Tạo chi tiết đợt thành toán thành công."
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

                    if (detail.PaymentStage.HasValue)
                    {
                        existingDetail.PaymentStage = detail.PaymentStage.Value;
                    }
                    if (detail.Percentage.HasValue)
                    {
                        existingDetail.Percentage = detail.Percentage.Value;
                    }
                    if (detail.Amount.HasValue)
                    {
                        existingDetail.Amount = detail.Amount.Value;
                    }
                    if (detail.PaymentProcessID.HasValue)
                    {
                        existingDetail.PaymentProcessID = detail.PaymentProcessID.Value;
                    }


                    _detailService.UpdatePaymentProcessDetail(existingDetail);

                    return Ok(new
                    {
                        message = "Cập nhật chi tiết đợt thành toán thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Chi tiết đợt thanh toán không tồn tại."
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
                        message = "Xóa chi tiết đợt thành toán thành công."
                    });
                }

                return NotFound(new
                {
                    message = "Chi tiết đợt thanh toán không tồn tại."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
