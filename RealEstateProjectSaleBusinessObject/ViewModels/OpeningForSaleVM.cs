using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class OpeningForSaleVM
    {
        public Guid OpeningForSaleID { get; set; }
        public string DecisionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CheckinDate { get; set; }
        public string SaleType { get; set; }
        public string ReservationPrice { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public Guid ProjectID { get; set; }
        public string ProjectName { get; set; }
    }
}
