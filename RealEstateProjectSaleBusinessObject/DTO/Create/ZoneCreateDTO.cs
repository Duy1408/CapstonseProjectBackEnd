using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class ZoneCreateDTO
    {
        public Guid ZoneID { get; set; }
        public string ZoneName { get; set; }
        public IFormFile? ImageZone { get; set; }
        public bool Status { get; set; }
        public Guid ProjectID { get; set; }

    }
}
