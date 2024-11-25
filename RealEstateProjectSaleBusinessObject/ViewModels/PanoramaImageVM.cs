using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class PanoramaImageVM
    {
        public Guid PanoramaImageID { get; set; }
        public string Title { get; set; }
        public string? Image { get; set; }
        public Guid ProjectID { get; set; }


    }
}
