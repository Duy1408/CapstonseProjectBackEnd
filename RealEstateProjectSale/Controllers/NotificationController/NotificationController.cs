using AutoMapper;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Principal;

namespace RealEstateProjectSale.Controllers.NotificationController
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationServices _notiServices;
        private readonly IBookingServices _bookServices;
        private readonly ICustomerServices _customerServices;
        private readonly IProjectCategoryDetailServices _categoryDetailServices;
        private readonly IMapper _mapper;

        public NotificationController(INotificationServices notiServices, IMapper mapper, ICustomerServices customerServices,
             IBookingServices bookServices, IProjectCategoryDetailServices categoryDetailServices)
        {
            _notiServices = notiServices;
            _customerServices = customerServices;
            _bookServices = bookServices;
            _categoryDetailServices = categoryDetailServices;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All Notification")]
        public IActionResult GetAllNotification()
        {
            try
            {
                if (_notiServices.GetAllNotification() == null)
                {
                    return NotFound(new
                    {
                        message = "Thông báo không tồn tại."
                    });
                }
                var notis = _notiServices.GetAllNotification();
                var response = _mapper.Map<List<NotificationVM>>(notis);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Notification By ID")]
        public IActionResult GetNotificationByID(Guid id)
        {
            var noti = _notiServices.GetNotificationByID(id);

            if (noti != null)
            {
                var responese = _mapper.Map<NotificationVM>(noti);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Thông báo không tồn tại."
            });

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new comment")]
        public IActionResult AddNewNotification(NotificationCreateDTO noti)
        {
            try
            {
                var booking = _bookServices.GetBookingById(noti.BookingID);
                if (booking == null)
                {
                    return NotFound(new
                    {
                        message = "Booking không tồn tại."
                    });
                }

                var newNoti = new NotificationCreateDTO
                {
                    NotificationID = Guid.NewGuid(),
                    Title = noti.Title,
                    Subtiltle = noti.Subtiltle,
                    Body = noti.Body,
                    CreatedTime = DateTime.Now,
                    Status = true,
                    BookingID = noti.BookingID,
                    CustomerID = booking.CustomerID
                };

                var notification = _mapper.Map<RealEstateProjectSaleBusinessObject.BusinessObject.Notification>(newNoti);
                _notiServices.AddNewNotification(notification);

                return Ok(new
                {
                    message = "Tạo thông báo thành công"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("send-ios/{bookingId}")]
        public async Task<IActionResult> SendNotification(Guid bookingId)
        {
            var booking = _bookServices.GetBookingById(bookingId);
            if (booking == null)
            {
                return NotFound(new { message = "Booking không tồn tại" });
            }

            var customer = _customerServices.GetCustomerByID(booking.CustomerID);
            if (customer == null)
            {
                return NotFound(new { message = "Khách hàng không tồn tại" });
            }
            if (string.IsNullOrEmpty(customer.DeviceToken))
            {
                return BadRequest(new { message = "Customer does not have a valid device token." });
            }

            var categoryDetail = _categoryDetailServices.GetProjectCategoryDetailByID(booking.ProjectCategoryDetailID);
            string title = "⏰ Đã tới thời gian Check-in";
            string body = "🏘️ Hãy check-in đúng giờ để nhận ưu đãi đặc biệt và thông tin chi tiết dự án " + categoryDetail.Project!.ProjectName;
            string subtitle = "Hãy sẵn sàng khám phá cơ hội đầu tư tốt nhất của quý khách "
                + customer.FullName + "về loại dự án " + categoryDetail.PropertyCategory!.PropertyCategoryName + " !";

            var message = new Message()
            {
                Token = customer.DeviceToken,
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = title,
                    Body = body
                },

                Data = new Dictionary<string, string>
                {
                    { "link", "justhome://booking?bookingID=" + bookingId },  // Truyền DeepLink vào payload của thông báo\
                    { "subtitle", subtitle }
                },

                Apns = new ApnsConfig
                {
                    Aps = new Aps
                    {
                        ContentAvailable = true,
                        Sound = "default"
                    }
                }
            };

            try
            {
                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);

                var newNoti = new NotificationCreateDTO
                {
                    NotificationID = Guid.NewGuid(),
                    Title = title,
                    Subtiltle = subtitle,
                    Body = body,
                    CreatedTime = DateTime.Now,
                    Status = true,
                    BookingID = bookingId,
                    CustomerID = booking.CustomerID
                };
                var _noti = _mapper.Map<RealEstateProjectSaleBusinessObject.BusinessObject.Notification>(newNoti);
                _notiServices.AddNewNotification(_noti);

                return Ok(new { message = "Gửi thông báo thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Notification by ID")]
        public IActionResult UpdateNotification([FromForm] NotificationUpdateDTO noti, Guid id)
        {
            try
            {
                var existingNoti = _notiServices.GetNotificationByID(id);
                if (existingNoti != null)
                {

                    if (!string.IsNullOrEmpty(noti.Title))
                    {
                        existingNoti.Title = noti.Title;
                    }
                    if (!string.IsNullOrEmpty(noti.Subtiltle))
                    {
                        existingNoti.Subtiltle = noti.Subtiltle;
                    }
                    if (!string.IsNullOrEmpty(noti.Body))
                    {
                        existingNoti.Body = noti.Body;
                    }
                    if (noti.Status.HasValue)
                    {
                        existingNoti.Status = noti.Status.Value;
                    }
                    if (noti.BookingID.HasValue)
                    {
                        existingNoti.BookingID = noti.BookingID.Value;
                    }
                    if (noti.CustomerID.HasValue)
                    {
                        existingNoti.CustomerID = noti.CustomerID.Value;
                    }

                    _notiServices.UpdateNotification(existingNoti);

                    return Ok(new
                    {
                        message = "Cập nhật thông báo thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Thông báo không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Notification")]
        public IActionResult DeleteNotification(Guid id)
        {

            var noti = _notiServices.GetNotificationByID(id);
            if (noti == null)
            {
                return NotFound(new
                {
                    message = "Thông báo không tồn tại."
                });
            }

            _notiServices.ChangeStatusNotification(noti);

            return Ok(new
            {
                message = "Xóa thông báo thành công"
            });
        }

    }
}
