using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class OpenForSaleDetailUpdateDTO
    {
        [JsonIgnore]
        public Guid OpenForSaleDetailID { get; set; }
        public int Floor { get; set; }
        public string TypeRoom { get; set; }
        public double Price { get; set; }
        [JsonIgnore]
        public Guid OpeningForSaleID { get; set; }
        [JsonIgnore]
        public Guid PropertiesID { get; set; }
    }
}
