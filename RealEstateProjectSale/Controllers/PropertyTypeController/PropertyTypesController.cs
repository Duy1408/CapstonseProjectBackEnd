using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleServices.IServices;

namespace RealEstateProjectSale.Controllers.PropertyTypeController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyTypesController : ControllerBase
    {
        private readonly IPropertyTypeServices _type;

        public PropertyTypesController(IPropertyTypeServices type)
        {
           _type = type;
        }

        // GET: api/PropertyTypes
        [HttpGet]
        public ActionResult<IEnumerable<PropertyType>> GetPropertiesTypes()
        {
          if (_type.GetPropertyType() == null)
          {
              return NotFound();
          }
            return _type.GetPropertyType().ToList();
        }

        // GET: api/PropertyTypes/5
        [HttpGet("{id}")]
        public ActionResult<PropertyType> GetPropertyType(Guid id)
        {
          if (_type.GetPropertyType() == null)
          {
              return NotFound();
          }
            var propertyType = _type.GetPropertyTypeById(id);

            if (propertyType == null)
            {
                return NotFound();
            }

            return propertyType;
        }

        // PUT: api/PropertyTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutPropertyType(Guid id, PropertyType propertyType)
        {
            if (_type.GetPropertyType() == null)
            {
                return BadRequest();
            }


            try
            {
                _type.UpdatePropertyType(propertyType);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_type.GetPropertyType() == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PropertyTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PropertyType>> PostPropertyType(PropertyType propertyType)
        {
          if (_type.GetPropertyType() == null)
          {
              return Problem("Entity set 'RealEstateProjectSaleSystemDBContext.PropertiesTypes'  is null.");
          }
           _type.AddNew(propertyType);

            return CreatedAtAction("GetPropertyType", new { id = propertyType.PropertyTypeID }, propertyType);
        }

        // DELETE: api/PropertyTypes/5
        [HttpDelete("{id}")]
        public IActionResult DeletePropertyType(Guid id)
        {
            if (_type.GetPropertyType() == null)
            {
                return NotFound();
            }
            var propertyType = _type.GetPropertyTypeById(id);
            if (propertyType == null)
            {
                return NotFound();
            }

           _type.ChangeStatus(propertyType);    

            return NoContent();
        }

      
    }
}
