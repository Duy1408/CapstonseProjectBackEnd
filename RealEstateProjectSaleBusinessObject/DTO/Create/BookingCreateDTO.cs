using RealEstateProjectSaleBusinessObject.Enums;
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
        public DateTime? DepositedTimed { get; set; }
        public double? DepositedPrice { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string? BookingFile { get; set; }
        public string? Note { get; set; }
        public string Status { get; set; }
        public Guid CustomerID { get; set; }
        public Guid? StaffID { get; set; }
        public Guid OpenForSaleID { get; set; }
        public Guid PropertyCategoryID { get; set; }
        public Guid? DocumentID { get; set; }
    }
}
