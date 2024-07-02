using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleServices.IServices;

namespace RealEstateProjectSale.Controllers.PaymentProcessController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentProcessesController : ControllerBase
    {
        private readonly IPaymentProcessServices _pmt;

        public PaymentProcessesController(IPaymentProcessServices pmt)
        {
            _pmt = pmt;
        }

        // GET: api/PaymentProcesses
        [HttpGet]
        public ActionResult<IEnumerable<PaymentProcess>> GetPaymentProcesses()
        {
          if (_pmt.GetPaymentProcess()==null)
          {
              return NotFound();
          }
            return _pmt.GetPaymentProcess().ToList();
        }

        // GET: api/PaymentProcesses/5
        [HttpGet("{id}")]
        public ActionResult<PaymentProcess> GetPaymentProcess(Guid id)
        {
          if (_pmt.GetPaymentProcess() == null)
          {
              return NotFound();
          }
            var paymentProcess = _pmt.GetPaymentProcessById(id);

            if (paymentProcess == null)
            {
                return NotFound();
            }

            return paymentProcess;
        }

        // PUT: api/PaymentProcesses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutPaymentProcess(Guid id, PaymentProcess paymentProcess)
        {
            if (_pmt.GetPaymentProcess() == null)
            {
                return BadRequest();
            }

           

            try
            {
                _pmt.UpdatePaymentProcess(paymentProcess);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_pmt.GetPaymentProcess() == null)
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

        // POST: api/PaymentProcesses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<PaymentProcess> PostPaymentProcess(PaymentProcess paymentProcess)
        {
          if (_pmt.GetPaymentProcess() == null)
          {
              return Problem("Entity set 'RealEstateProjectSaleSystemDBContext.PaymentProcesses'  is null.");
          }
            _pmt.AddNew(paymentProcess);

            return CreatedAtAction("GetPaymentProcess", new { id = paymentProcess.PaymentProcessID }, paymentProcess);
        }

        // DELETE: api/PaymentProcesses/5
        [HttpDelete("{id}")]
        public IActionResult DeletePaymentProcess(Guid id)
        {
            if (_pmt.GetPaymentProcess() == null)
            {
                return NotFound();
            }
            var paymentProcess = _pmt.GetPaymentProcessById(id);
            if (paymentProcess == null)
            {
                return NotFound();
            }

            _pmt.ChangeStatus(paymentProcess);

            return NoContent();
        }

       
    }
}
