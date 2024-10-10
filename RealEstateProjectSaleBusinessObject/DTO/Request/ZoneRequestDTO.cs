using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Request
{
    public class ZoneRequestDTO
    {
        public string ZoneName { get; set; }
        public IFormFileCollection? ImageZone { get; set; }
        public bool Status { get; set; }
    }
}
