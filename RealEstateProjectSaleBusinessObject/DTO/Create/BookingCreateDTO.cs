using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class BookingCreateDTO
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
        public Guid? PropertyID { get; set; }
        public Guid OpeningForSaleID { get; set; }
        public Guid ProjectID { get; set; }
        public Guid CustomerID { get; set; }
        public Guid? StaffID { get; set; }
    }
}
