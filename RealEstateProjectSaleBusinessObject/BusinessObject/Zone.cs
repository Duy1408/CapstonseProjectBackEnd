using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class Zone
    {
        public Guid ZoneID { get; set; }
        public string ZoneName { get; set; }
        public string? ImageZone { get; set; }
        public Guid PropertyCategoryy { get; set; }
        public PropertyCategory? PropertyCategory { get; set; }
        public List<Property>? Properties {get;set;}
        public List<Block>? Blocks { get; set; }

    }
}
