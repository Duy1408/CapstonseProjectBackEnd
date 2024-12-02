using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class PropertyTypeCreateDTO
    {
        [JsonIgnore]
        public Guid PropertyTypeID { get; set; }
        public string? PropertyTypeName { get; set; }
        public Guid PropertyCategoryID { get; set; }
    }
}
