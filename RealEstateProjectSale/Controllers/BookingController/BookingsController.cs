using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.Enums;
using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;
using Stripe;
using Swashbuckle.AspNetCore.Annotations;
using static System.Reflection.Metadata.BlobBuilder;

namespace RealEstateProjectSale.Controllers.BookingController
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingServices _book;
        private readonly IFileUploadToBlobService _fileService;
        private readonly IDocumentTemplateService _documentService;
        private readonly IOpeningForSaleServices _openService;
        private readonly ICustomerServices _customerServices;
        private readonly IProjectServices _projectService;
        private readonly IProjectCategoryDetailServices _detailServices;
        private readonly IMapper _mapper;

        public BookingsController(IBookingServices book, IFileUploadToBlobService fileService,
                    IMapper mapper, IDocumentTemplateService documentService, IOpeningForSaleServices openService,
                    ICustomerServices customerServices, IProjectServices projectService, IProjectCategoryDetailServices detailServices)
        {
            _book = book;
            _fileService = fileService;
            _mapper = mapper;
            _documentService = documentService;
            _openService = openService;
            _customerServices = customerServices;
            _projectService = projectService;
            _detailServices = detailServices;
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

        [HttpGet("customer/{customerId}")]
        [SwaggerOperation(Summary = "Get Booking by customer ID")]
        public IActionResult GetBookingByCustomerID(Guid customerId)
        {
            var book = _book.GetBookingByCustomerID(customerId);

            if (book != null)
            {
                var responese = book.Select(book => _mapper.Map<BookingVM>(book)).ToList();

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Booking not found."
            });

        }

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
                string? blobUrl = null;
                var bookingFile = book.BookingFile;
                if (bookingFile != null)
                {
                    using (var pdfStream = bookingFile.OpenReadStream())
                    {
                        blobUrl = _fileService.UploadSingleFile(pdfStream, bookingFile.FileName, "bookingfile");
                    }
                }

                var existingBook = _book.GetBookingById(id);
                if (existingBook != null)
                {
                    if (!string.IsNullOrEmpty(book.Note))
                    {
                        existingBook.Note = book.Note;
                    }
                    if (!string.IsNullOrEmpty(book.Status) && int.TryParse(book.Status, out int statusValue))
                    {
                        if (Enum.IsDefined(typeof(BookingStatus), statusValue))
                        {
                            var statusEnum = (BookingStatus)statusValue;
                            var statusDescription = statusEnum.GetEnumDescription();
                            existingBook.Status = statusDescription;
                        }
                    }
                    if (blobUrl != null)
                    {
                        existingBook.BookingFile = blobUrl;
                    }
                    if (book.StaffID.HasValue)
                    {
                        existingBook.StaffID = book.StaffID.Value;
                    }
                    if (book.CustomerID.HasValue)
                    {
                        existingBook.CustomerID = book.CustomerID.Value;
                    }
                    if (book.OpeningForSaleID.HasValue)
                    {
                        existingBook.OpeningForSaleID = book.OpeningForSaleID.Value;
                    }
                    if (book.ProjectCategoryDetailID.HasValue)
                    {
                        existingBook.ProjectCategoryDetailID = book.ProjectCategoryDetailID.Value;
                    }
                    if (book.DocumentTemplateID.HasValue)
                    {
                        existingBook.DocumentTemplateID = book.DocumentTemplateID.Value;
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

                    book.Status = BookingStatus.DaCheckIn.GetEnumDescription();

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
        public ActionResult<Booking> AddNew(Guid categoryDetailID, Guid customerID)
        {
            try
            {
                var existingCustomer = _customerServices.GetCustomerByID(customerID);
                if (existingCustomer == null)
                {
                    return NotFound(new
                    {
                        message = "Customer not found."
                    });
                }

                var existingDetail = _detailServices.GetProjectCategoryDetailByID(categoryDetailID);
                if (existingDetail == null)
                {
                    return NotFound(new
                    {
                        message = "ProjectCategoryDetail not found."
                    });
                }

                //var existingCategoryDetail = _detailServices.GetProjectCategoryDetailByID(projectID, propertyCategoryID);
                //if (existingCategoryDetail == null)
                //{
                //    return NotFound(new
                //    {
                //        message = "Project Category not found."
                //    });
                //}

                var openForSale = _openService.FindByDetailIdAndStatus(categoryDetailID);

                //var existingBooking = _book.CheckExistingBooking(openForSale.OpeningForSaleID, projectID, customerID);
                //if (existingBooking != null)
                //{
                //    return BadRequest(new
                //    {
                //        message = "Customer has booked this project."
                //    });
                //}

                var documentReservation = _documentService.GetDocumentByDocumentName("Phiếu giữ chỗ");

                var newbook = new BookingCreateDTO
                {
                    BookingID = Guid.NewGuid(),
                    DepositedTimed = null,
                    DepositedPrice = null,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = null,
                    BookingFile = null,
                    Note = null,
                    Status = BookingStatus.ChuaThanhToanTienGiuCho.GetEnumDescription(),
                    CustomerID = customerID,
                    StaffID = null,
                    ProjectCategoryDetailID = categoryDetailID,
                    OpeningForSaleID = openForSale.OpeningForSaleID,
                    DocumentTemplateID = documentReservation.DocumentTemplateID,
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

        //[HttpGet("generate-pdf")]
        //public IActionResult GeneratePdfDocument(Guid templateId)
        //{
        //    try
        //    {
        //        var documentTemplate = _documentService.GetDocumentById(templateId);

        //        var htmlContent = _book.GenerateDocumentContent(templateId);

        //        var pdfBytes = _documentService.GeneratePdfFromTemplate(htmlContent);

        //        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
        //        {
        //            string? blobUrl = null;
        //            blobUrl = _fileService.UploadSingleFile(pdfStream, documentTemplate.DocumentName, "bookingfile");

        //            return Ok(new
        //            {
        //                url = blobUrl
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

    }
}
