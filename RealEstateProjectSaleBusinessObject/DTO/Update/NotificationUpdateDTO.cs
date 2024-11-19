using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class NotificationUpdateDTO
    {
        public string? Title { get; set; }
        public string? Subtiltle { get; set; }
        public string? Body { get; set; }
        public bool? Status { get; set; }
        public Guid? BookingID { get; set; }
        public Guid? CustomerID { get; set; }
    }
}
