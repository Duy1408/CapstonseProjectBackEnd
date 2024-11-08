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

namespace RealEstateProjectSale.Controllers.PaymentProcessController
{
    [Route("api/payment-processes")]
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
        [SwaggerOperation(Summary = "Get All PaymentProcess")]
        public IActionResult GetAllPaymentProcess()
        {
            try
            {
                if (_pmtService.GetPaymentProcess() == null)
                {
                    return NotFound(new
                    {
                        message = "PaymentProcess not found."
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
                message = "PaymentProcess not found."
            });

        }

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
                    Status = process.Status,
                    SalesPolicyID = process.SalesPolicyID
                };

                var _process = _mapper.Map<PaymentProcess>(newProcess);
                _pmtService.AddNew(_process);

                return Ok(new
                {
                    message = "Create PaymentProcess Successfully"
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
                        message = "Update PaymentProcess Successfully"
                    });
                }

                return NotFound(new
                {
                    message = "PaymentProcess not found."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
                        message = "Delete PaymentProcess Successfully"
                    });
                }

                return NotFound(new
                {
                    message = "PaymentProcess not found."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
