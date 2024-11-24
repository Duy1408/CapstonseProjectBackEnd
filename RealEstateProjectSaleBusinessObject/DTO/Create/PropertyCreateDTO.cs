using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class PropertyCreateDTO
    {
        [JsonIgnore]
        public Guid PropertyID { get; set; }
        public string PropertyCode { get; set; }
        public string? View { get; set; }
        public double? PriceSold { get; set; }
        [JsonIgnore]
        public string? Status { get; set; }
        public Guid? UnitTypeID { get; set; }
        public Guid? FloorID { get; set; }
        public Guid? BlockID { get; set; }
        public Guid? ZoneID { get; set; }
        public Guid? ProjectCategoryDetailID { get; set; }


    }
}
