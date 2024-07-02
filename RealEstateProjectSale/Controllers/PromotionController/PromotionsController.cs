using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleServices.IServices;

namespace RealEstateProjectSale.Controllers.PromotionController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly IPromotionServices _pro;

        public PromotionsController(IPromotionServices pro)
        {
            _pro = pro;
        }

        // GET: api/Promotions
        [HttpGet]
        public ActionResult<IEnumerable<Promotion>> GetPromotions()
        {
          if (_pro.GetPromotions() == null)
          {
              return NotFound();
          }
            return _pro.GetPromotions().ToList();
        }

        // GET: api/Promotions/5
        [HttpGet("{id}")]
        public ActionResult<Promotion> GetPromotion(Guid id)
        {
          if (_pro.GetPromotions() == null)
          {
              return NotFound();
          }
            var promotion = _pro.GetPromotionById(id);

            if (promotion == null)
            {
                return NotFound();
            }

            return promotion;
        }

        // PUT: api/Promotions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPromotion(Guid id, Promotion promotion)
        {
            if (_pro.GetPromotions() == null)
            {
                return BadRequest();
            }
           

            try
            {
                _pro.UpdatePromotion(promotion);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_pro.GetPromotions() == null)
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

        // POST: api/Promotions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Promotion> PostPromotion(Promotion promotion)
        {
          if (_pro.GetPromotions() == null)
          {
              return Problem("Entity set 'RealEstateProjectSaleSystemDBContext.Promotions'  is null.");
          }
            _pro.AddNew(promotion);

            return CreatedAtAction("GetPromotion", new { id = promotion.PromotionID }, promotion);
        }

        // DELETE: api/Promotions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotion(Guid id)
        {
            if (_pro.GetPromotions() == null)
            {
                return NotFound();
            }
            var promotion = _pro.GetPromotionById(id);
            if (promotion == null)
            {
                return NotFound();
            }

            _pro.ChangeStatus(promotion);

            return NoContent();
        }

      
    }
}
