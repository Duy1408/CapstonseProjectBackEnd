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
        public double? Price { get; set; }
        public string? Note { get; set; }
    }
}
