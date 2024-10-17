using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.Enums;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;
using Swashbuckle.AspNetCore.Annotations;
using static System.Reflection.Metadata.BlobBuilder;

namespace RealEstateProjectSale.Controllers.BookingController
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingServices _book;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IMapper _mapper;

        public BookingsController(IBookingServices book, BlobServiceClient blobServiceClient, IMapper mapper)
        {
            _book = book;
            _blobServiceClient = blobServiceClient;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "GetAllBooking")]
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

        [HttpGet("booked")]
        [SwaggerOperation(Summary = "Get bookings by status 'Booked'")]
        public IActionResult GetBookingByBooked()
        {
            try
            {
                if (_book.GetBookingByBooked() == null)
                {
                    return NotFound();
                }
                var books = _book.GetBookingByBooked();
                var response = _mapper.Map<List<BookingVM>>(books);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("checked-in")]
        [SwaggerOperation(Summary = "Get bookings by status 'Checked In'")]
        public IActionResult GetBookingByCheckedIn()
        {
            try
            {
                if (_book.GetBookingByCheckedIn() == null)
                {
                    return NotFound();
                }
                var books = _book.GetBookingByCheckedIn();
                var response = _mapper.Map<List<BookingVM>>(books);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Booking By ID")]
        public IActionResult GetBookingByID(Guid id)
        {
            var book = _book.GetBookingById(id);

            if (book != null)
            {
                var responese = _mapper.Map<BookingVM>(book);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Booking not found."
            });

        }

        // GET: api/Bookings/5
        [HttpGet("deposits/{numberOfBookings}")]
        [SwaggerOperation(Summary = "Get booking by deposit times")]
        public ActionResult<Booking> GetBookingByDepositedTimed(int numberOfBookings)
        {
            if (_book.GetBookings() == null)
            {
                return NotFound(new
                {
                    message = "Booking not found."
                });
            }
            var bookingList = _book.GetBookingByDepositedTimed(numberOfBookings);

            if (bookingList == null)
            {
                return NotFound(new
                {
                    message = "Booking By DepositedTimed not found."
                });
            }

            var booking = bookingList.FirstOrDefault();

            var response = _mapper.Map<BookingVM>(booking);

            return Ok(response);

        }

        [HttpGet("random/{numberBooking}")]
        [SwaggerOperation(Summary = "Get random bookings")]
        public ActionResult<Booking> GetBookingByRandom(int numberBooking)
        {
            if (_book.GetBookings() == null)
            {
                return NotFound(new
                {
                    message = "Booking not found."
                });
            }
            var bookingList = _book.GetBookingByRandom(numberBooking);

            if (bookingList == null)
            {
                return NotFound(new
                {
                    message = "Booking By Random not found."
                });
            }

            var booking = bookingList.FirstOrDefault();

            var response = _mapper.Map<BookingVM>(booking);

            return Ok(response);

        }


        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update booking by ID")]
        public IActionResult UpdateBooking([FromForm] BookingUpdateDTO book, Guid id)
        {
            try
            {
                var containerInstance = _blobServiceClient.GetBlobContainerClient("bookingfile");
                string? blobUrl = null;
                if (book.BookingFile != null)
                {
                    var blobName = $"{Guid.NewGuid()}_{book.BookingFile.FileName}";
                    var blobInstance = containerInstance.GetBlobClient(blobName);
                    blobInstance.Upload(book.BookingFile.OpenReadStream());
                    var storageAccountUrl = "https://realestatesystem.blob.core.windows.net/bookingfile";
                    blobUrl = $"{storageAccountUrl}/{blobName}";
                }

                var existingBook = _book.GetBookingById(id);
                if (existingBook != null)
                {
                    if (!string.IsNullOrEmpty(book.Note))
                    {
                        existingBook.Note = book.Note;
                    }
                    if (!string.IsNullOrEmpty(book.Status))
                    {
                        existingBook.Status = book.Status;
                    }
                    if (blobUrl != null)
                    {
                        existingBook.BookingFile = blobUrl;
                    }
                    if (book.StaffID.HasValue)
                    {
                        existingBook.StaffID = book.StaffID.Value;
                    }

                    existingBook.UpdatedTime = DateTime.Now;
                    _book.UpdateBooking(existingBook);

                    return Ok(new
                    {
                        message = "Update Booking Successfully"
                    });

                }

                return NotFound(new
                {
                    message = "Booking not found."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/check-in")]
        [SwaggerOperation(Summary = "Mark customer as checked in")]
        public IActionResult CustomerCheckedInBooking(Guid id)
        {
            try
            {
                var book = _book.GetBookingById(id);
                if (book != null)
                {

                    book.Status = BookingStatus.CheckedIn.ToString();

                    _book.UpdateBooking(book);

                    return Ok(new
                    {
                        message = "Customer checked in Successfully"
                    });

                }

                return NotFound(new
                {
                    message = "Booking not found."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new booking")]
        public ActionResult<Booking> AddNew(Guid openForSaleID,
                    Guid propertyCategoryID, Guid projectID, Guid customerID)
        {
            try
            {
                var newbook = new BookingCreateDTO
                {
                    BookingID = Guid.NewGuid(),
                    DepositedTimed = null,
                    DepositedPrice = null,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = null,
                    BookingFile = null,
                    Note = null,
                    Status = BookingStatus.NotDeposited.ToString(),
                    CustomerID = customerID,
                    StaffID = null,
                    ProjectID = projectID,
                    OpeningForSaleID = openForSaleID,
                    PropertyCategoryID = propertyCategoryID,
                    DocumentID = null,
                    PropertyID = null
                };

                var books = _mapper.Map<Booking>(newbook);
                _book.AddNew(books);

                return Ok(new
                {
                    message = "Create Booking Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
