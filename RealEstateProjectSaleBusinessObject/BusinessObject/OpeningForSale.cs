using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class OpeningForSale
    {
        public Guid OpeningForSaleID { get; set; }
        public string DecisionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double ReservationPrice { get; set; }
        public string? Description { get; set; }
        public DateTime CheckinDate { get; set; }
        public string SaleType { get; set; }
        public bool Status { get; set; }
        public List<Booking>? Bookings { get; set; }
        public List<OpenForSaleDetail>? OpenForSaleDetails { get; set; }
        public Guid ProjectCategoryDetailID { get; set; }
        public ProjectCategoryDetail? ProjectCategoryDetail { get; set; }



    }
}
