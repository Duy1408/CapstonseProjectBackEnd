using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;

namespace RealEstateProjectSale.Controllers.PaymentTypeController
{
    [Route("api/[controller]")]
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
        public IActionResult GetAllPaymentType()
        {
            try
            {
                if (_typeServices.GetAllPaymentType() == null)
                {
                    return NotFound();
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
        public IActionResult GetPaymentTypeByID(Guid id)
        {
            var type = _typeServices.GetPaymentTypeByID(id);

            if (type != null)
            {
                var responese = _mapper.Map<PaymentTypeVM>(type);

                return Ok(responese);
            }

            return NotFound();

        }

        [HttpPost]
        public IActionResult AddNewPaymentType(PaymentTypeCreateDTO type)
        {
            try
            {
                var _type = _mapper.Map<PaymentType>(type);
                _typeServices.AddNewPaymentType(_type);

                return Ok("Create PaymentType Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePaymentType(PaymentTypeUpdateDTO type, Guid id)
        {
            try
            {
                var existingType = _typeServices.GetPaymentTypeByID(id);
                if (existingType != null)
                {
                    type.PaymentTypeID = existingType.PaymentTypeID;

                    var _type = _mapper.Map<PaymentType>(type);
                    _typeServices.UpdatePaymentType(_type);

                    return Ok("Update Successfully");

                }

                return NotFound("PaymentType not found.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePaymentType(Guid id)
        {
            try
            {
                var type = _typeServices.GetPaymentTypeByID(id);
                if (type != null)
                {
                    _typeServices.DeletePaymentTypeByID(id);
                    return Ok("Delete PaymentType Successfully");
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
