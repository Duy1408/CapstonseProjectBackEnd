using System;
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
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.PaymentProcessController
{
    [Route("api/payment-processes")]
    [ApiController]
    public class PaymentProcessesController : ControllerBase
    {
        private readonly IPaymentProcessServices _pmtService;
        private readonly IPaymentProcessDetailServices _detailService;
        private readonly IMapper _mapper;

        public PaymentProcessesController(IPaymentProcessServices pmtService, IPaymentProcessDetailServices detailService,
            IMapper mapper)
        {
            _pmtService = pmtService;
            _detailService = detailService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [SwaggerOperation(Summary = "Get All PaymentProcess")]
        public IActionResult GetAllPaymentProcess()
        {
            try
            {
                if (_pmtService.GetPaymentProcess() == null)
                {
                    return NotFound(new
                    {
                        message = "Đợt thanh toán không tồn tại."
                    });
                }
                var process = _pmtService.GetPaymentProcess();
                var response = _mapper.Map<List<PaymentProcessVM>>(process);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get PaymentProcess by ID")]
        public IActionResult GetPaymentProcessByID(Guid id)
        {
            var process = _pmtService.GetPaymentProcessById(id);

            if (process != null)
            {
                var responese = _mapper.Map<PaymentProcessVM>(process);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Đợt thanh toán không tồn tại."
            });

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("salesPolicy/{salesPolicyId}")]
        [SwaggerOperation(Summary = "Get PaymentProcess By SalePolicyID")]
        public IActionResult GetPaymentProcessBySalesPolicyID(Guid salesPolicyId)
        {
            var pmt = _pmtService.GetPaymentProcessBySalesPolicyID(salesPolicyId);

            if (pmt != null)
            {
                var responese = _mapper.Map<List<PaymentProcessVM>>(pmt);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Chính sách bán hàng không tồn tại."
            });

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new PaymentProcess")]
        public IActionResult AddNew(PaymentProcessCreateDTO process)
        {
            try
            {
                var newProcess = new PaymentProcessCreateDTO
                {
                    PaymentProcessID = Guid.NewGuid(),
                    PaymentProcessName = process.PaymentProcessName,
                    Status = true,
                    SalesPolicyID = process.SalesPolicyID
                };

                var _process = _mapper.Map<PaymentProcess>(newProcess);
                _pmtService.AddNew(_process);

                var newDetail = new PaymentProcessDetailCreateDTO
                {
                    PaymentProcessDetailID = Guid.NewGuid(),
                    PaymentStage = 1,
                    Description = "Ký TTDC",
                    DurationDate = null,
                    Percentage = null,
                    Amount = 100000000,
                    PaymentProcessID = newProcess.PaymentProcessID
                };

                var _detail = _mapper.Map<PaymentProcessDetail>(newDetail);
                _detailService.AddNew(_detail);

                return Ok(new
                {
                    message = "Tạo đợt thành toán thành công."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update PaymentProcess by ID")]
        public IActionResult UpdatePaymentProcess([FromForm] PaymentProcessUpdateDTO process, Guid id)
        {
            try
            {
                var existingProcess = _pmtService.GetPaymentProcessById(id);
                if (existingProcess != null)
                {

                    if (!string.IsNullOrEmpty(process.PaymentProcessName))
                    {
                        existingProcess.PaymentProcessName = process.PaymentProcessName;
                    }
                    if (process.Status.HasValue)
                    {
                        existingProcess.Status = process.Status.Value;
                    }
                    if (process.SalesPolicyID.HasValue)
                    {
                        existingProcess.SalesPolicyID = process.SalesPolicyID.Value;
                    }

                    _pmtService.UpdatePaymentProcess(existingProcess);

                    return Ok(new
                    {
                        message = "Cập nhật đợt thành toán thành công."
                    });
                }

                return NotFound(new
                {
                    message = "Đợt thanh toán không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete PaymentProcess by ID")]
        public IActionResult DeletePaymentProcessByID(Guid id)
        {
            try
            {
                var process = _pmtService.GetPaymentProcessById(id);
                if (process != null)
                {
                    _pmtService.DeletePaymentProcessByID(id);
                    return Ok(new
                    {
                        message = "Xóa đợt thành toán thành công."
                    });
                }

                return NotFound(new
                {
                    message = "Đợt thanh toán không tồn tại."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
