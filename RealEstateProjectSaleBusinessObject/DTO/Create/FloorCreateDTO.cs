using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class FloorCreateDTO
    {
        public Guid FloorID { get; set; }
        public int NumFloor { get; set; }
        public IFormFile? ImageFloor { get; set; }
        public bool Status { get; set; }
        public Guid BlockID { get; set; }
    }
}
