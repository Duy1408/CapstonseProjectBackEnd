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
        public string DescriptionName { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string? ReservationTime { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public bool Status { get; set; }
        public Guid ProjectID { get; set; }
    }
}
