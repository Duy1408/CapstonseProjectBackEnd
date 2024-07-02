using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleServices.IServices;

namespace RealEstateProjectSale.Controllers.PaymentProcessDetailController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentProcessDetailsController : ControllerBase
    {
        private readonly IPaymentProcessDetailServices _pmt;

        public PaymentProcessDetailsController(IPaymentProcessDetailServices pmt)
        {
            _pmt = pmt;
        }

        // GET: api/PaymentProcessDetails
        [HttpGet]
        public ActionResult<IEnumerable<PaymentProcessDetail>> GetPaymentProcessDetails()
        {
          if (_pmt.GetPaymentProcessDetail()==null)
          {
              return NotFound();
          }
            return _pmt.GetPaymentProcessDetail().ToList();
        }

        // GET: api/PaymentProcessDetails/5
        [HttpGet("{id}")]
        public ActionResult<PaymentProcessDetail> GetPaymentProcessDetail(Guid id)
        {
          if (_pmt.GetPaymentProcessDetail() == null)
          {
              return NotFound();
          }
            var paymentProcessDetail = _pmt.GetPaymentProcessDetailById(id);

            if (paymentProcessDetail == null)
            {
                return NotFound();
            }

            return paymentProcessDetail;
        }

        // PUT: api/PaymentProcessDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutPaymentProcessDetail(Guid id, PaymentProcessDetail paymentProcessDetail)
        {
            if (_pmt.GetPaymentProcessDetail() == null)
            {
                return BadRequest();
            }

           

            try
            {
                _pmt.UpdatePaymentProcessDetail(paymentProcessDetail);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_pmt.GetPaymentProcessDetail() == null)
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

        // POST: api/PaymentProcessDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<PaymentProcessDetail> PostPaymentProcessDetail(PaymentProcessDetail paymentProcessDetail)
        {
          if (_pmt.GetPaymentProcessDetail() == null)
          {
              return Problem("Entity set 'RealEstateProjectSaleSystemDBContext.PaymentProcessDetails'  is null.");
          }
            _pmt.AddNew(paymentProcessDetail);

            return CreatedAtAction("GetPaymentProcessDetail", new { id = paymentProcessDetail.PaymentProcessDetailID }, paymentProcessDetail);
        }

        // DELETE: api/PaymentProcessDetails/5
        [HttpDelete("{id}")]
        public IActionResult DeletePaymentProcessDetail(Guid id)
        {
            if (_pmt.GetPaymentProcessDetail() == null)
            {
                return NotFound();
            }
            var paymentProcessDetail = _pmt.GetPaymentProcessDetailById(id);
            if (paymentProcessDetail == null)
            {
                return NotFound();
            }

            _pmt.ChangeStatus(paymentProcessDetail);

            return NoContent();
        }

   
    }
}
