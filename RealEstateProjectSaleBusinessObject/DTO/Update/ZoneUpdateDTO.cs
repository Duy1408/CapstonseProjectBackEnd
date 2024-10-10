using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class ZoneUpdateDTO
    {
        public string ZoneName { get; set; }
        public IFormFileCollection? ImageZone { get; set; }
        public bool? Status { get; set; }
        public Guid? ProjectID { get; set; }
    }
}
