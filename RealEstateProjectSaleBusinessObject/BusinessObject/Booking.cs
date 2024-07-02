using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class Booking
    {
        public Guid BookingID { get; set; }
        public string Address { get; set; }
        public DateTime Dateofsignature { get; set; }
        public byte[] BookingFile { get; set; }
        public bool Status { get; set; }
        public List<Payment>? Payments { get; set; }
        public Guid PropertyID { get; set; }
        public Property? Property { get; set; }
        public Guid OpeningForSaleID { get; set; }
        public OpeningForSale? OpeningForSale { get; set; }
        public Guid ProjectID { get; set; }
        public Project? Project { get; set; }
        public Guid CustomerID { get; set; }
        public Customer? Customer { get; set; }
        public Guid StaffID { get; set; }
        public Staff? Staff { get; set; }

   
    }
}
