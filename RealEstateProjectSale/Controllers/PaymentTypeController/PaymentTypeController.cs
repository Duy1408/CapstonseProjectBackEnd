using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.PaymentTypeController
{
    [Route("api/payment-types")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentTypeServices _typeServices;
        private readonly IMapper _mapper;

        public PaymentTypeController(IPaymentTypeServices typeServices, IMapper mapper)
        {
            _typeServices = typeServices;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All PaymentType")]
        public IActionResult GetAllPaymentType()
        {
            try
            {
                if (_typeServices.GetAllPaymentType() == null)
                {
                    return NotFound(new
                    {
                        message = "PaymentType not found."
                    });
                }
                var types = _typeServices.GetAllPaymentType();
                var response = _mapper.Map<List<PaymentTypeVM>>(types);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get PaymentType by ID")]
        public IActionResult GetPaymentTypeByID(Guid id)
        {
            var type = _typeServices.GetPaymentTypeByID(id);

            if (type != null)
            {
                var responese = _mapper.Map<PaymentTypeVM>(type);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "PaymentType not found."
            });

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new PaymentType")]
        public IActionResult AddNewPaymentType(PaymentTypeCreateDTO type)
        {
            try
            {
                var newType = new PaymentTypeCreateDTO
                {
                    PaymentTypeID = Guid.NewGuid(),
                    PaymentName = type.PaymentName
                };

                var _type = _mapper.Map<PaymentType>(newType);
                _typeServices.AddNewPaymentType(_type);

                return Ok(new
                {
                    message = "Create PaymentType Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update PaymentType by ID")]
        public IActionResult UpdatePaymentType([FromForm] PaymentTypeUpdateDTO type, Guid id)
        {
            try
            {
                var existingType = _typeServices.GetPaymentTypeByID(id);
                if (existingType != null)
                {
                    if (!string.IsNullOrEmpty(type.PaymentName))
                    {
                        existingType.PaymentName = type.PaymentName;
                    }

                    _typeServices.UpdatePaymentType(existingType);

                    return Ok(new
                    {
                        message = "Update PaymentType Successfully"
                    });

                }

                return NotFound(new
                {
                    message = "PaymentType not found."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete PaymentType by ID")]
        public IActionResult DeletePaymentType(Guid id)
        {
            try
            {
                var type = _typeServices.GetPaymentTypeByID(id);
                if (type != null)
                {
                    _typeServices.DeletePaymentTypeByID(id);
                    return Ok(new
                    {
                        message = "Delete PaymentType Successfully"
                    });
                }

                return NotFound(new
                {
                    message = "PaymentType not found."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
