using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class ProjectCategoryDetailCreateDTO
    {
        [JsonIgnore]
        public Guid ProjectCategoryDetailID { get; set; }
        public Guid ProjectID { get; set; }
        public Guid PropertyCategoryID { get; set; }
    }
}
