using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleServices.IServices;

namespace RealEstateProjectSale.Controllers.SalespolicyController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalespoliciesController : ControllerBase
    {
        private readonly ISalespolicyServices _sale;

        public SalespoliciesController(ISalespolicyServices sale)
        {
            _sale = sale;
        }

        // GET: api/Salespolicies
        [HttpGet]
        public ActionResult<IEnumerable<Salespolicy>> GetSalespolicies()
        {
          if (_sale.GetSalespolicys() == null)
          {
              return NotFound();
          }
            return _sale.GetSalespolicys().ToList();
        }

        // GET: api/Salespolicies/5
        [HttpGet("{id}")]
        public ActionResult<Salespolicy> GetSalespolicy(Guid id)
        {
          if (_sale.GetSalespolicys() == null)
          {
              return NotFound();
          }
            var salespolicy = _sale.GetSalespolicyById(id);

            if (salespolicy == null)
            {
                return NotFound();
            }

            return salespolicy;
        }

        // PUT: api/Salespolicies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutSalespolicy(Guid id, Salespolicy salespolicy)
        {
            if (_sale.GetSalespolicys() == null)
            {
                return BadRequest();
            }

           

            try
            {
                _sale.UpdateSalespolicy(salespolicy);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_sale.GetSalespolicys() == null)
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

        // POST: api/Salespolicies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Salespolicy> PostSalespolicy(Salespolicy salespolicy)
        {
          if (_sale.GetSalespolicys() == null)
          {
              return Problem("Entity set 'RealEstateProjectSaleSystemDBContext.Salespolicies'  is null.");
          }
            _sale.AddNew(salespolicy);

            return CreatedAtAction("GetSalespolicy", new { id = salespolicy.SalesPolicyID }, salespolicy);
        }

        // DELETE: api/Salespolicies/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSalespolicy(Guid id)
        {
            if (_sale.GetSalespolicys() == null)
            {
                return NotFound();
            }
            var salespolicy = _sale.GetSalespolicyById(id);
            if (salespolicy == null)
            {
                return NotFound();
            }

            _sale.ChangeStatus(salespolicy);

            return NoContent();
        }

       
    }
}
