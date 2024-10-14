using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class UnitTypeCreateDTO
    {
        [JsonIgnore]
        public Guid UnitTypeID { get; set; }
        public int BathRoom { get; set; }
        public IFormFile? Image { get; set; }
        public double? NetFloorArea { get; set; }
        public double? GrossFloorArea { get; set; }
        public int BedRoom { get; set; }
        public int KitchenRoom { get; set; }
        public int LivingRoom { get; set; }
        public int? NumberFloor { get; set; }
        public int? Basement { get; set; }
        [JsonIgnore]
        public bool Status { get; set; }
        public Guid? PropertyTypeID { get; set; }
        public Guid ProjectID { get; set; }
    }
}
