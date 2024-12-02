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
using RealEstateProjectSaleServices.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.PropertyTypeController
{
    [Route("api/property-types")]
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
        [SwaggerOperation(Summary = "Get All PropertyType")]
        public IActionResult GetAllPropertyType()
        {
            try
            {
                if (_type.GetAllPropertyType() == null)
                {
                    return NotFound(new
                    {
                        message = "Loại căn không tồn tại."
                    });
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

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get PropertyType by ID")]
        public IActionResult GetPropertyTypeByID(Guid id)
        {
            var type = _type.GetPropertyTypeByID(id);

            if (type != null)
            {
                var responese = _mapper.Map<PropertyTypeVM>(type);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Loại căn không tồn tại."
            });

        }

        [HttpGet("property-category/{categoryId}")]
        [SwaggerOperation(Summary = "Get Property Type By PropertyCategoryID")]
        public IActionResult GetPropertyTypeByPropertyCategoryID(Guid categoryId)
        {
            var type = _type.GetPropertyTypeByPropertyCategoryID(categoryId);

            if (type != null)
            {
                var responese = _mapper.Map<List<PropertyTypeVM>>(type);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Loại căn không tồn tại."
            });

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new PropertyType")]
        public IActionResult AddNew(PropertyTypeCreateDTO type)
        {
            try
            {

                var newType = new PropertyTypeCreateDTO
                {
                    PropertyTypeID = Guid.NewGuid(),
                    PropertyTypeName = type.PropertyTypeName,
                    PropertyCategoryID = type.PropertyCategoryID
                };

                var propertyType = _mapper.Map<PropertyType>(newType);
                _type.AddNew(propertyType);

                return Ok(new
                {
                    message = "Tạo loại căn thành công."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update PropertyType by ID")]
        public IActionResult UpdatePropertyType([FromForm] PropertyTypeUpdateDTO type, Guid id)
        {
            try
            {
                var existingType = _type.GetPropertyTypeByID(id);
                if (existingType != null)
                {

                    if (!string.IsNullOrEmpty(type.PropertyTypeName))
                    {
                        existingType.PropertyTypeName = type.PropertyTypeName;
                    }

                    if (type.PropertyCategoryID.HasValue)
                    {
                        existingType.PropertyCategoryID = type.PropertyCategoryID.Value;
                    }


                    _type.UpdatePropertyType(existingType);

                    return Ok(new
                    {
                        message = "Cập nhật loại căn thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Loại căn không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete PropertyType by ID")]
        public IActionResult DeletePropertyType(Guid id)
        {
            try
            {
                var type = _type.GetPropertyTypeByID(id);
                if (type != null)
                {
                    _type.DeletePropertyTypeByID(id);
                    return Ok(new
                    {
                        message = "Xóa loại căn thành công."
                    });
                }

                return NotFound(new
                {
                    message = "Loại căn không tồn tại."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
