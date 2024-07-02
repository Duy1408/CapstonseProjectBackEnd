using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleServices.IServices;

namespace RealEstateProjectSale.Controllers.OpeningForSaleController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpeningForSalesController : ControllerBase
    {
        private readonly IOpeningForSaleServices _open;

        public OpeningForSalesController(IOpeningForSaleServices open)
        {
            _open = open;
        }

        // GET: api/OpeningForSales
        //add new
        [HttpGet]
        public ActionResult<IEnumerable<OpeningForSale>> GetOpeningForSales()
        {
          if (_open.GetOpeningForSales() == null)
          {
              return NotFound();
          }
            return _open.GetOpeningForSales().ToList();
        }

        // GET: api/OpeningForSales/5
        [HttpGet("{id}")]
        public ActionResult<OpeningForSale> GetOpeningForSale(Guid id)
        {
          if (_open.GetOpeningForSales() == null)
          {
              return NotFound();
          }
            var openingForSale = _open.GetOpeningForSaleById(id);

            if (openingForSale == null)
            {
                return NotFound();
            }

            return openingForSale;
        }

        // PUT: api/OpeningForSales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutOpeningForSale(Guid id, OpeningForSale openingForSale)
        {
            if (_open.GetOpeningForSales() == null)
            {
                return BadRequest();
            }

           

            try
            {
                _open.UpdateOpeningForSale(openingForSale);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_open.GetOpeningForSales() == null)
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

        // POST: api/OpeningForSales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<OpeningForSale> PostOpeningForSale(OpeningForSale openingForSale)
        {
          if (_open.GetOpeningForSales() == null)
          {
              return Problem("Entity set 'RealEstateProjectSaleSystemDBContext.OpeningForSales'  is null.");
          }
            _open.AddNew(openingForSale);

            return CreatedAtAction("GetOpeningForSale", new { id = openingForSale.OpeningForSaleID }, openingForSale);
        }

        // DELETE: api/OpeningForSales/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOpeningForSale(Guid id)
        {
            if (_open.GetOpeningForSales() == null)
            {
                return NotFound();
            }
            var openingForSale = _open.GetOpeningForSaleById(id);
            if (openingForSale == null)
            {
                return NotFound();
            }

            _open.ChangeStatus(openingForSale);

            return NoContent();
        }

    
    }
}
