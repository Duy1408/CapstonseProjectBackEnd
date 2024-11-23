using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Request
{
    public class BlockRequestDTO
    {
        public string BlockName { get; set; }
        public IFormFileCollection? ImageBlock { get; set; }
        public Guid ZoneID { get; set; }
    }
}
