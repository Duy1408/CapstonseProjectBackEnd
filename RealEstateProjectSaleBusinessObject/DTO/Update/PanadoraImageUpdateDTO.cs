using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class PanadoraImageUpdateDTO
    {
    
        public string? Title { get; set; }
        public IFormFileCollection? Image { get; set; }
        public Guid? ProjectID { get; set; }
    }
}
