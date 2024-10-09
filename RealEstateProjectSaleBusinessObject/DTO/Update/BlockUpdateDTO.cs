using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class BlockUpdateDTO
    {
        public string? BlockName { get; set; }
        public IFormFileCollection? ImageBlock { get; set; }
        public bool? Status { get; set; }
        public Guid? ZoneID { get; set; }
    }
}
