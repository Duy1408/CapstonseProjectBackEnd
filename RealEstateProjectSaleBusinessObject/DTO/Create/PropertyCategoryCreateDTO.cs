using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class PropertyCategoryCreateDTO
    {
        [JsonIgnore]
        public Guid PropertyCategoryID { get; set; }
        public string? PropertyCategoryName { get; set; }
        [JsonIgnore]
        public bool Status { get; set; }
    }
}
