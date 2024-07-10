using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
using static System.Reflection.Metadata.BlobBuilder;

namespace RealEstateProjectSale.Controllers.BookingController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingServices _book;
        private readonly IMapper _mapper;

        public BookingsController(IBookingServices book, IMapper mapper)
        {
            _book = book;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllBooking")]
        public IActionResult GetAllBooking()
        {
            try
            {
                if (_book.GetBookings() == null)
                {
                    return NotFound();
                }
                var books = _book.GetBookings();
                var response = _mapper.Map<List<BookingVM>>(books);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetBookingByID/{id}")]
        public IActionResult GetBookingByID(Guid id)
        {
            var book = _book.GetBookingById(id);

            if (book != null)
            {
                var responese = _mapper.Map<BookingVM>(book);

                return Ok(responese);
            }

            return NotFound();

        }

        // GET: api/Bookings/5
        [HttpGet("GetBookingByDepositedTimed/{numberOfBookings}")]
        public ActionResult<Booking> GetBookingByDepositedTimed(int numberOfBookings)
        {
            if (_book.GetBookings() == null)
            {
                return NotFound();
            }
            var booking = _book.GetBookingByDepositedTimed(numberOfBookings);

            if (booking == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<List<BookingVM>>(booking);

            return Ok(response);

        }

        [HttpGet("GetBookingByRandom/{numberBooking}")]
        public ActionResult<Booking> GetBookingByRandom(int numberBooking)
        {
            if (_book.GetBookings() == null)
            {
                return NotFound();
            }
            var booking = _book.GetBookingByRandom(numberBooking);

            if (booking == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<List<BookingVM>>(booking);

            return Ok(response);

        }


        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateBooking/{id}")]
        public IActionResult UpdateBooking([FromForm] BookingUpdateDTO book, Guid id)
        {
            try
            {
                // Chuyển đổi IFormFile sang byte[]
                byte[]? bookFileBytes = null;
                if (book.BookingFile != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        book.BookingFile.CopyTo(ms);
                        bookFileBytes = ms.ToArray();
                    }
                }

                var existingBook = _book.GetBookingById(id);
                if (existingBook != null)
                {
                    if (bookFileBytes != null)
                    {
                        existingBook.BookingFile = bookFileBytes;
                    }
                    if (!string.IsNullOrEmpty(book.Note))
                    {
                        existingBook.Note = book.Note;
                    }
                    if (!string.IsNullOrEmpty(book.Status))
                    {
                        existingBook.Status = book.Status;
                    }
                    if (book.PropertyID.HasValue)
                    {
                        existingBook.PropertyID = book.PropertyID.Value;
                    }
                    if (book.StaffID.HasValue)
                    {
                        existingBook.StaffID = book.StaffID.Value;
                    }

                    existingBook.UpdatedTime = DateTime.Now;
                    _book.UpdateBooking(existingBook);

                    return Ok("Update Booking Successfully");

                }

                return NotFound("Booking not found.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("AddNewBooking")]
        public ActionResult<Booking> AddNew(Guid openingForSaleID,
                    Guid projectID, Guid customerID)
        {
            try
            {
                var newbook = new BookingCreateDTO
                {
                    BookingID = Guid.NewGuid(),
                    ReservationDate = DateTime.Now,
                    DepositedTimed = null,
                    DepositedPrice = null,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = null,
                    BookingFile = null,
                    Note = null,
                    Status = "Chưa đặt cọc tiền giữ chỗ",
                    PropertyID = null,
                    OpeningForSaleID = openingForSaleID,
                    ProjectID = projectID,
                    CustomerID = customerID,
                    StaffID = null
                };

                var books = _mapper.Map<Booking>(newbook);
                _book.AddNew(books);

                return Ok("Create Booking Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
