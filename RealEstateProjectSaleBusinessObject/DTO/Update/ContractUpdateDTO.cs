using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class ContractUpdateDTO
    {
        [JsonIgnore]
        public Guid ContractID { get; set; }
        public DateTime? DateSigned { get; set; }
        public DateTime? UpdateUsAt { get; set; }
        public DateTime CreatedStAt { get; set; }
        public string ContractType { get; set; }
        public bool Status { get; set; }
        [JsonIgnore]
        public Guid BookingID { get; set; }
    }
}
