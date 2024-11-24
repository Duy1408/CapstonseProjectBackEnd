using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class OpenForSaleDetailCreateDTO
    {
        public Guid OpeningForSaleID { get; set; }
        public Guid PropertyID { get; set; }
        public double? Price { get; set; }
        public string? Note { get; set; }

    }
}
