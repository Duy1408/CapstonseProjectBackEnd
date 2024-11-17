using AutoMapper;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;
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
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(INotificationServices notiServices, IMapper mapper, ICustomerServices customerServices,
             IBookingServices bookServices, ILogger<NotificationController> logger)
        {
            _notiServices = notiServices;
            _customerServices = customerServices;
            _bookServices = bookServices;
            _mapper = mapper;
            _logger = logger;
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

        [HttpPost("send-ios")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
        {
            var booking = _bookServices.GetBookingById(request.BookingID);
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

            var message = new Message()
            {
                Token = customer.DeviceToken,
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = request.Title,
                    Body = request.Body
                },

                Data = new Dictionary<string, string>
                {
                    { "link", "justhome://realtime?projectCategoryDetail=" + booking.ProjectCategoryDetailID },  // Truyền DeepLink vào payload của thông báo\
                    { "subtitle", request.Subtiltle }
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
                    Title = request.Title,
                    Subtiltle = request.Subtiltle,
                    Body = request.Body,
                    //DeepLink = request.DeepLink,
                    BookingID = request.BookingID
                };
                var _noti = _mapper.Map<RealEstateProjectSaleBusinessObject.BusinessObject.Notification>(newNoti);
                _notiServices.AddNewNotification(_noti);

                return Ok(new { message = "Gửi thông báo thành công.", response });
            }
            catch (FirebaseMessagingException fme)
            {
                // Log lỗi chi tiết của FirebaseMessaging
                _logger.LogError(fme, "FirebaseMessagingException: {Message}", fme.Message);
                return BadRequest(new { message = "Error sending notification", error = fme.Message });
            }
            catch (Exception ex)
            {
                // Log lỗi chung
                _logger.LogError(ex, "General Exception: {Message}", ex.Message);
                return BadRequest(new { message = "Error sending notification", error = ex.Message });
            }
        }



    }
}
