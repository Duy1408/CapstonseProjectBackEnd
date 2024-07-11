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

namespace RealEstateProjectSale.Controllers.PaymentProcessController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentProcessesController : ControllerBase
    {
        private readonly IPaymentProcessServices _pmtService;
        private readonly IMapper _mapper;

        public PaymentProcessesController(IPaymentProcessServices pmtService, IMapper mapper)
        {
            _pmtService = pmtService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllPaymentProcess")]
        public IActionResult GetAllPaymentProcess()
        {
            try
            {
                if (_pmtService.GetPaymentProcess() == null)
                {
                    return NotFound();
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

        [HttpGet("GetPaymentProcessByID/{id}")]
        public IActionResult GetPaymentProcessByID(Guid id)
        {
            var process = _pmtService.GetPaymentProcessById(id);

            if (process != null)
            {
                var responese = _mapper.Map<PaymentProcessVM>(process);

                return Ok(responese);
            }

            return NotFound();

        }

        [HttpPost]
        [Route("AddNewPaymentProcess")]
        public IActionResult AddNew(PaymentProcessCreateDTO process)
        {
            try
            {
                var newProcess = new PaymentProcessCreateDTO
                {
                    PaymentProcessID = Guid.NewGuid(),
                    PaymentProcessName = process.PaymentProcessName,
                    Discount = process.Discount,
                    TotalPrice = process.TotalPrice,
                    SalesPolicyID = process.SalesPolicyID
                };

                var _process = _mapper.Map<PaymentProcess>(newProcess);
                _pmtService.AddNew(_process);

                return Ok("Create PaymentProcess Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdatePaymentProcess/{id}")]
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
                    if (process.Discount.HasValue)
                    {
                        existingProcess.Discount = process.Discount.Value;
                    }
                    if (process.TotalPrice.HasValue)
                    {
                        existingProcess.TotalPrice = process.TotalPrice.Value;
                    }

                    _pmtService.UpdatePaymentProcess(existingProcess);

                    return Ok("Update PaymentProcess Successfully");

                }

                return NotFound("PaymentProcess not found.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeletePaymentProcessByID/{id}")]
        public IActionResult DeletePaymentProcessByID(Guid id)
        {
            try
            {
                var process = _pmtService.GetPaymentProcessById(id);
                if (process != null)
                {
                    _pmtService.DeletePaymentProcessByID(id);
                    return Ok("Deleted PaymentProcess Successfully");
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
