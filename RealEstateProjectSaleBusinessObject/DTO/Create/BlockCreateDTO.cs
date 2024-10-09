using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class BlockCreateDTO
    {
        public Guid BlockID { get; set; }
        public string BlockName { get; set; }
        public IFormFile? ImageBlock { get; set; }
        public bool Status { get; set; }
        public Guid ZoneID { get; set; }
    }
}
