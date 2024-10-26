using AutoMapper;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.NotificationController
{
    [Route("api/[controller]")]
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





    }
}
