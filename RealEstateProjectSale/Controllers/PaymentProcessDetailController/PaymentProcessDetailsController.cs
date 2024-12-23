﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Stripe;
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new PaymentProcessDetail")]
        public IActionResult AddNew(PaymentProcessDetailCreateDTO detail)
        {
            try
            {
                var existingPaymentStage = _detailService.CheckPaymentStage(detail.PaymentProcessID, detail.PaymentStage);
                if (existingPaymentStage != null)
                {
                    return BadRequest(new
                    {
                        message = "Đợt thanh toán này đã tồn tại trong Chi tiết đợt thanh toán."
                    });
                }

                var totalPercentage = _detailService.GetTotalPercentageByPaymentProcessID(detail.PaymentProcessID)
                                            + detail.Percentage;
                if (totalPercentage > 1)
                {
                    return BadRequest(new
                    {
                        message = "Phần trăm đã lớn hơn 1."
                    });
                }

                var newDetail = new PaymentProcessDetailCreateDTO
                {
                    PaymentProcessDetailID = Guid.NewGuid(),
                    PaymentStage = detail.PaymentStage,
                    Description = detail.Description,
                    DurationDate = detail.DurationDate,
                    Percentage = detail.Percentage,
                    Amount = detail.Amount,
                    PaymentProcessID = detail.PaymentProcessID
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

        [Authorize(Roles = "Admin")]
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
                    if (!string.IsNullOrEmpty(detail.Description))
                    {
                        existingDetail.Description = detail.Description;
                    }
                    if (detail.Percentage.HasValue)
                    {
                        existingDetail.Percentage = detail.Percentage.Value;
                    }
                    if (detail.DurationDate.HasValue)
                    {
                        existingDetail.DurationDate = detail.DurationDate.Value;
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

        [Authorize(Roles = "Admin")]
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
