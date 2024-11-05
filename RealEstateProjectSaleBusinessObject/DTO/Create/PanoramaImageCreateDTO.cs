using Microsoft.AspNetCore.Http;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class PanoramaImageCreateDTO
    {
        public Guid PanoramaImageID { get; set; }
        public string Title { get; set; }
        public IFormFile? Image { get; set; }
        public Guid ProjectID { get; set; }
   
    }
}
