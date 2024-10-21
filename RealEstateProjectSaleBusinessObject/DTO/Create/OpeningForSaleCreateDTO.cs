using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class OpeningForSaleCreateDTO
    {
        [JsonIgnore]
        public Guid OpeningForSaleID { get; set; }
        public string DecisionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CheckinDate { get; set; }
        public string SaleType { get; set; }
        public double ReservationPrice { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public bool Status { get; set; }
        public Guid ProjectID { get; set; }
    }
}
