using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class BookingVM
    {
        public Guid BookingID { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime? DepositedTimed { get; set; }
        public double? DepositedPrice { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public byte[]? BookingFile { get; set; }
        public string? Note { get; set; }
        public string Status { get; set; }
        public string PropertyName { get; set; }
        public string DescriptionName { get; set; }
        public string ProjectName { get; set; }
        public string PersonalEmailCs { get; set; }
        public string PersonalEmailSt { get; set; }
    }
}
