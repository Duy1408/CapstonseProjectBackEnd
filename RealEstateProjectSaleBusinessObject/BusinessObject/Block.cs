using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class Block
    {
        public Guid BlockID { get; set; }
        public string BlockName { get; set; }
        public string? ImageBlock { get; set; }
        public bool? Status { get; set; }
        public Guid ZoneID { get; set; }
        public Zone? Zone { get; set; }
        public List<Property>? Properties { get; set; }
        public List<Floor>? Floors { get; set; }
    }
}
