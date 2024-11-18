using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class ZoneVM
    {
        public Guid ZoneID { get; set; }
        public string ZoneName { get; set; }
        public string? ImageZone { get; set; }
        public Guid ProjectID { get; set; }
        public string ProjectName { get; set; }

    }
}
