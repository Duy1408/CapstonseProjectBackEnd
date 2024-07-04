using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleServices.IServices;

namespace RealEstateProjectSale.Controllers.BookingController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingServices _book;

        public BookingsController(IBookingServices book)
        {
            _book = book;
        }

        // GET: api/Bookings
        [HttpGet]
        public ActionResult<IEnumerable<Booking>> GetBookings()
        {
          if (_book.GetBookings()==null)
          {
              return NotFound();
          }
            return _book.GetBookings().ToList();
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public ActionResult<Booking> GetBooking(Guid id)
        {
          if (_book.GetBookings() == null)
          {
              return NotFound();
          }
            var booking = _book.GetBookingById(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // GET: api/Bookings/5
        [HttpGet("{numberOfBookings}")]
        public ActionResult<Booking> GetBooking(int numberOfBookings)
        {
            if (_book.GetBookings() == null)
            {
                return NotFound();
            }
            var booking = _book.GetBookingByNumber(numberOfBookings);

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }


        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutBooking(Guid id, Booking booking)
        {
            if (_book.GetBookings() == null)
            {
                return BadRequest();
            }

       
            try
            {
                _book.UpdateBooking(booking);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_book.GetBookings() == null)
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

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Booking> PostBooking(Booking booking)
        {
          if (_book.GetBookings() == null)
          {
              return Problem("Entity set 'RealEstateProjectSaleSystemDBContext.Bookings'  is null.");
          }
            _book.AddNew(booking);

            return CreatedAtAction("GetBooking", new { id = booking.BookingID }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(Guid id)
        {
            if (_book.GetBookings() == null)
            {
                return NotFound();
            }
            var booking = _book.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }

            _book.ChangeStatus(booking);

            return NoContent();
        }

       
    }
}
