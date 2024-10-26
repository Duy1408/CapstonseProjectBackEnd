using AutoMapper;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Principal;

namespace RealEstateProjectSale.Controllers.NotificationController
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationServices _notiServices;
        private readonly IMapper _mapper;

        public NotificationController(INotificationServices notiServices, IMapper mapper)
        {
            _notiServices = notiServices;
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
                        message = "Notification not found."
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
                message = "Notification not found."
            });

        }

        [HttpPost("send-ios")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
        {
            var message = new Message()
            {
                Token = request.DeviceToken,
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = request.Title,
                    Body = request.Body
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
                    DeepLink = request.DeepLink,
                    CustomerID = request.CustomerID,
                    OpeningForSaleID = request.OpeningForSaleID,
                };
                var _noti = _mapper.Map<RealEstateProjectSaleBusinessObject.BusinessObject.Notification>(newNoti);
                _notiServices.AddNewNotification(_noti);

                return Ok(new { message = "Notification sent successfully", response });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error sending notification", error = ex.Message });
            }
        }



    }
}
