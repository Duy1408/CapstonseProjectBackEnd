using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class PropertyUpdateDTO
    {
        public string? PropertyCode { get; set; }
        public string? View { get; set; }
        public double? PriceSold { get; set; }

        public string? Status { get; set; }
        public Guid? UnitTypeID { get; set; }
        public Guid? FloorID { get; set; }
        public Guid? BlockID { get; set; }
        public Guid? ZoneID { get; set; }

    }
}
