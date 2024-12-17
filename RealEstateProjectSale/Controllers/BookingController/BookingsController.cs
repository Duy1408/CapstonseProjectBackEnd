using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<PropertyHub> _hubContext;


        public BookingsController(IBookingServices book, IFileUploadToBlobService fileService,
                    IMapper mapper, IDocumentTemplateService documentService, IOpeningForSaleServices openService,
                    ICustomerServices customerServices, IProjectServices projectService, IProjectCategoryDetailServices detailServices,
                    IHubContext<PropertyHub> hubContext)
        {
            _book = book;
            _fileService = fileService;
            _mapper = mapper;
            _documentService = documentService;
            _openService = openService;
            _customerServices = customerServices;
            _projectService = projectService;
            _detailServices = detailServices;
            _hubContext = hubContext;
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet]
        [SwaggerOperation(Summary = "GetAllBooking")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách Booking.", typeof(List<BookingVM>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Booking không tồn tại.")]
        public IActionResult GetAllBooking()
        {
            try
            {
                if (_book.GetBookings() == null)
                {
                    return NotFound(new
                    {
                        message = "Booking không tồn tại."
                    });
                }
                var books = _book.GetBookings().OrderByDescending(b => b.CreatedTime).ToList();
                var response = _mapper.Map<List<BookingVM>>(books);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("booked")]
        [SwaggerOperation(Summary = "Get bookings by status 'Booked'")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách Booking.", typeof(List<BookingVM>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Booking không tồn tại.")]
        public IActionResult GetBookingByBooked()
        {
            try
            {
                if (_book.GetBookingByBooked() == null)
                {
                    return NotFound(new
                    {
                        message = "Booking không tồn tại."
                    });
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

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("checked-in/{openId}")]
        [SwaggerOperation(Summary = "Get bookings by status 'Checked In'")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách Booking.", typeof(List<BookingVM>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Booking không tồn tại.")]
        public IActionResult GetBookingByCheckedIn(Guid openId)
        {
            try
            {
                if (_book.GetBookingByCheckedIn(openId) == null)
                {
                    return NotFound(new
                    {
                        message = "Booking không tồn tại."
                    });
                }
                var books = _book.GetBookingByCheckedIn(openId);
                var response = _mapper.Map<List<BookingVM>>(books);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("property/{propertyid}")]
        [SwaggerOperation(Summary = "Get bookings by propertyid")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách Booking.", typeof(BookingVM))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Booking không tồn tại.")]
        public IActionResult GetBookingPropertyID(Guid propertyid)
        {
            try
            {
                if (_book.GetBookings() == null)
                {
                    return NotFound(new
                    {
                        message = "Booking không tồn tại."
                    });
                }
                var book = _book.GetBookingByPropertyID(propertyid);
                var response = _mapper.Map<BookingVM>(book);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Booking By ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách Booking.", typeof(BookingVM))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Booking không tồn tại.")]
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
                message = "Booking không tồn tại."
            });

        }

        [HttpGet("customer/{customerId}")]
        [SwaggerOperation(Summary = "Get Booking by customer ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách Booking.", typeof(List<BookingVM>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Booking không tồn tại.")]
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
                message = "Booking không tồn tại."
            });

        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("open-for-sale/{openId}")]
        [SwaggerOperation(Summary = "Get Booking By OpeningForSaleID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách Booking.", typeof(List<BookingVM>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Booking không tồn tại.")]
        public IActionResult GetBookingByOpeningForSaleID(Guid openId)
        {
            var book = _book.GetBookingByOpeningForSaleID(openId);

            if (book != null)
            {
                var responese = book.Select(book => _mapper.Map<BookingVM>(book)).ToList();

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Booking không tồn tại."
            });

        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("staff/{staffId}")]
        [SwaggerOperation(Summary = "Get Booking by staff ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách Booking.", typeof(List<BookingVM>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Booking không tồn tại.")]
        public IActionResult GetBookingByStaffID(Guid staffId)
        {
            var book = _book.GetBookingByStaffID(staffId);

            if (book != null)
            {
                var responese = book.Select(book => _mapper.Map<BookingVM>(book)).ToList();

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Booking không tồn tại."
            });

        }

        [HttpGet("deposits/{projectCategoryDetailId}")]
        [SwaggerOperation(Summary = "Get booking by deposit times By ProjectCategoryDetailID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách Booking.", typeof(BookingVM))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Booking không tồn tại.")]
        public ActionResult<Booking> GetBookingByDepositedTimed(Guid projectCategoryDetailId)
        {
            var booking = _book.GetBookingByDepositedTimed(projectCategoryDetailId);

            if (booking == null)
            {
                return NotFound(new
                {
                    message = "Booking không tồn tại."
                });
            }

            var response = _mapper.Map<BookingVM>(booking);

            return Ok(response);

        }

        [HttpGet("random/{projectCategoryDetailId}")]
        [SwaggerOperation(Summary = "Get random bookings By ProjectCategoryDetailID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách Booking.", typeof(BookingVM))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Booking không tồn tại.")]
        public ActionResult<Booking> GetBookingByRandom(Guid projectCategoryDetailId)
        {
            var booking = _book.GetBookingByRandom(projectCategoryDetailId);

            if (booking == null)
            {
                return NotFound(new
                {
                    message = "Booking không tồn tại."
                });
            }

            var response = _mapper.Map<BookingVM>(booking);

            return Ok(response);

        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update booking by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Cập nhật Booking thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Booking không tồn tại.")]
        public IActionResult UpdateBooking([FromForm] BookingUpdateDTO book, Guid id)
        {
            try
            {
                string? blobUrl = null;
                if (book.RefundImage != null)
                {
                    blobUrl = _fileService.UploadSingleImage(book.RefundImage, "refundimage");
                }

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
                    if (blobUrl != null)
                    {
                        existingBook.RefundImage = blobUrl;
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
                        message = "Cập nhật Booking thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Booking không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/check-in")]
        [SwaggerOperation(Summary = "Mark customer as checked in")]
        [SwaggerResponse(StatusCodes.Status200OK, "Customer đã check-in thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Booking không tồn tại.")]
        public IActionResult CustomerCheckedInBooking(Guid id)
        {
            try
            {
                var book = _book.GetBookingById(id);
                if (book != null)
                {
                    if (book.Status != BookingStatus.DaDatCho.GetEnumDescription() && book.Status != BookingStatus.DaCheckIn.GetEnumDescription())
                    {
                        return BadRequest(new
                        {
                            message = "Customer chưa thanh toán tiền đặt cọc giữ chỗ."
                        });
                    }

                    var openStatus = _openService.GetOpenForSaleStatusByProjectCategoryDetailID(book.ProjectCategoryDetailID);
                    if (openStatus != OpeningForSaleStatus.CheckIn.GetEnumDescription())
                    {
                        return BadRequest(new
                        {
                            message = "Loại dự án chưa tới thời gian check in."
                        });
                    }

                    book.Status = BookingStatus.DaCheckIn.GetEnumDescription();
                    book.CheckInTime = DateTime.Now;
                    _book.UpdateBooking(book);

                    return Ok(new
                    {
                        message = "Customer đã check-in thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Booking không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new booking")]
        [SwaggerResponse(StatusCodes.Status200OK, "Tạo Booking thành công")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Thông tin cần thiết đã tồn tại.")]
        public ActionResult<Booking> AddNew(Guid categoryDetailID, Guid customerID)
        {
            try
            {
                var existingCustomer = _customerServices.GetCustomerByID(customerID);
                if (existingCustomer == null)
                {
                    return NotFound(new
                    {
                        message = "Customer không tồn tại."
                    });
                }

                var existingIdentification = _customerServices.CheckCustomerByIdentification(customerID);
                if (existingIdentification != null)
                {
                    return NotFound(new
                    {
                        message = "Customer chưa cập nhật giấy tờ tùy thân."
                    });
                }

                var existingDetail = _detailServices.GetProjectCategoryDetailByID(categoryDetailID);
                if (existingDetail == null)
                {
                    return NotFound(new
                    {
                        message = "ProjectCategoryDetail không tồn tại."
                    });
                }

                var openForSale = _openService.FindByDetailIdAndStatus(categoryDetailID);

                if (openForSale == null)
                {
                    return NotFound(new
                    {
                        message = "OpenForSale không tồn tại."
                    });
                }

                var openStatus = _openService.GetOpenForSaleStatusByProjectCategoryDetailID(openForSale.ProjectCategoryDetailID);
                if (openStatus == OpeningForSaleStatus.ChuaMoBan.GetEnumDescription())
                {
                    return BadRequest(new
                    {
                        message = "Loại dự án chưa cho giữ chỗ."
                    });
                }
                if (openStatus == OpeningForSaleStatus.CheckIn.GetEnumDescription())
                {
                    return BadRequest(new
                    {
                        message = "Loại dự án đang trong thời gian check in để chọn căn."
                    });
                }

                var existingBooking = _book.CheckExistingBooking(openForSale.OpeningForSaleID, categoryDetailID, customerID);
                if (existingBooking != null)
                {
                    return BadRequest(new
                    {
                        message = "Customer đã đặt chỗ cho Loại dự án này."
                    });
                }

                var documentReservation = _documentService.GetDocumentByDocumentName("Phiếu giữ chỗ");
                if (documentReservation == null)
                {
                    return NotFound(new
                    {
                        message = "Document không tồn tại."
                    });
                }

                if (openStatus == OpeningForSaleStatus.MuaTrucTiep.GetEnumDescription())
                {
                    var newBookDirect = new BookingCreateDTO
                    {
                        BookingID = Guid.NewGuid(),
                        DepositedTimed = DateTime.Now,
                        DepositedPrice = null,
                        CreatedTime = DateTime.Now,
                        UpdatedTime = null,
                        BookingFile = null,
                        Note = null,
                        Status = BookingStatus.DaCheckIn.GetEnumDescription(),
                        CustomerID = customerID,
                        StaffID = null,
                        ProjectCategoryDetailID = categoryDetailID,
                        OpeningForSaleID = openForSale.OpeningForSaleID,
                        DocumentTemplateID = documentReservation.DocumentTemplateID,
                        PropertyID = null
                    };

                    var booksDirect = _mapper.Map<Booking>(newBookDirect);
                    _book.AddNew(booksDirect);

                    return Ok(new
                    {
                        message = "Tạo Booking thành công."
                    });

                }

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
                    message = "Tạo Booking thành công"
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("not-choose/{id}")]
        [SwaggerOperation(Summary = "Customer Not Choose Property")]
        [SwaggerResponse(StatusCodes.Status200OK, "Cập nhật trạng thái booking thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Booking không tồn tại.")]
        public async Task<IActionResult> CustomerNotChooseProperty(Guid id)
        {

            var booking = _book.GetBookingById(id);
            if (booking == null)
            {
                return NotFound(new
                {
                    message = "Booking không tồn tại."
                });
            }

            booking.Status = BookingStatus.KhongChonSanPham.GetEnumDescription();
            _book.UpdateBooking(booking);

            await _hubContext.Clients.All.SendAsync("ReceiveBookingStatus", booking.BookingID.ToString(), booking.Status);
            return Ok(new
            {
                message = "Cập nhật trạng thái booking thành công."
            });
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Booking by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Xóa Booking thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Booking không tồn tại.")]
        public IActionResult DeleteBooking(Guid id)
        {

            var booking = _book.GetBookingById(id);
            if (booking == null)
            {
                return NotFound(new
                {
                    message = "Booking không tồn tại."
                });
            }

            _book.ChangeStatusBooking(booking);

            return Ok(new
            {
                message = "Xóa Booking thành công."
            });
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpPut("upload-payment-order/{id}")]
        [SwaggerOperation(Summary = "Staff upload PaymentOrder refund")]
        [SwaggerResponse(StatusCodes.Status200OK, "Cập nhật Booking thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Booking không đủ điều kiện để thực hiện refund.")]
        public IActionResult StaffUploadPaymentOrder([FromForm] UploadRefundImageDTO book, Guid id)
        {
            try
            {

                var existingBook = _book.GetBookingById(id);
                if (existingBook != null && existingBook.Status == BookingStatus.KhongChonSanPham.GetEnumDescription() && existingBook.RefundImage == null)
                {

                    string? blobUrl = null;
                    if (book.RefundImage != null)
                    {
                        blobUrl = _fileService.UploadSingleImage(book.RefundImage, "refundimage");
                    }
                    existingBook.Status = BookingStatus.Dahoantien.GetEnumDescription();
                    existingBook.UpdatedTime = DateTime.Now;
                    _book.UpdateBooking(existingBook);
                    return Ok(new
                    {
                        message = "Upload thành công ủy nhiệm chi"
                    });
                }
                return NotFound(new
                {
                    message = "Không đủ điều kiện để thực hiện hoàn tiền."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
