using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Request
{
    public class NotificationRequest
    {
        public string Title { get; set; }
        public string Subtiltle { get; set; }
        public string Body { get; set; }
        public string DeepLink { get; set; }
        public Guid BookingID { get; set; }
    }
}
