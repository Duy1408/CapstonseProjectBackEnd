using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;

namespace RealEstateProjectSale.Controllers.PropertyTypeController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyTypesController : ControllerBase
    {
        private readonly IPropertyTypeServices _type;
        private readonly IMapper _mapper;

        public PropertyTypesController(IPropertyTypeServices type, IMapper mapper)
        {
            _type = type;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllPropertyType")]
        public IActionResult GetAllPropertyType()
        {
            try
            {
                if (_type.GetAllPropertyType() == null)
                {
                    return NotFound();
                }
                var types = _type.GetAllPropertyType();
                var response = _mapper.Map<List<PropertyTypeVM>>(types);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPropertyTypeByID/{id}")]
        public IActionResult GetPropertyTypeByID(Guid id)
        {
            var type = _type.GetPropertyTypeByID(id);

            if (type != null)
            {
                var responese = _mapper.Map<PropertyTypeVM>(type);

                return Ok(responese);
            }

            return NotFound();

        }

    }
}
