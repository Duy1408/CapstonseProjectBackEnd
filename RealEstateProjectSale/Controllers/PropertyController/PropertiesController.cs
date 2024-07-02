using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleServices.IServices;

namespace RealEstateProjectSale.Controllers.PropertyController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyServices _pro;

       
        public PropertiesController(IPropertyServices pro)
        {
            _pro = pro;
        }

        // GET: api/Properties
        [HttpGet]
        public ActionResult<IEnumerable<Property>> GetProperties()
        {
          if (_pro.GetProperty() == null)
          {
              return NotFound();
          }
            return _pro.GetProperty().ToList();
        }

        // GET: api/Properties/5
        [HttpGet("{id}")]
        public ActionResult<Property> GetProperty(Guid id)
        {
          if (_pro.GetProperty() == null)
          {
              return NotFound();
          }
            var property = _pro.GetPropertyById(id);

            if (property == null)
            {
                return NotFound();
            }

            return property;
        }

        // PUT: api/Properties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProperty(Guid id, Property property)
        {
            if (_pro.GetProperty()==null)
            {
                return BadRequest();
            }

           

            try
            {
                _pro.UpdateProperty(property);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_pro.GetProperty()==null)
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

        // POST: api/Properties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Property>> PostProperty(Property property)
        {
          if (_pro.GetProperty() == null)
          {
              return Problem("Entity set 'RealEstateProjectSaleSystemDBContext.Properties'  is null.");
          }
            _pro.AddNew(property);

            return CreatedAtAction("GetProperty", new { id = @property.PropertyID }, @property);
        }

        // DELETE: api/Properties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(Guid id)
        {
            if (_pro.GetProperty() == null)
            {
                return NotFound();
            }
            var property = _pro.GetPropertyById(id);
            if (property == null)
            {
                return NotFound();
            }
            _pro.ChangeStatus(property);

            return NoContent();
        }

    
    }
}
