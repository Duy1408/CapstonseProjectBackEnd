using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Request
{
    public class OpeningForSaleRequestDTO
    {
        public string DecisionName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CheckinDate { get; set; }
        public string SaleType { get; set; }
        public string ReservationPrice { get; set; }
        public string? Description { get; set; }
        public Guid ProjectID { get; set; }
    }
}
