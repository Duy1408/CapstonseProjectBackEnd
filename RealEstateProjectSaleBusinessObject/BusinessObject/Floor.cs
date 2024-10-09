using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class Floor
    {
        public Guid FloorID { get; set; }
        public int NumFloor { get; set; }
        public string? ImageFloor { get; set; }
        public bool Status { get; set; }
        public List<Property>? Properties { get; set; }
        public Guid BlockID { get; set; }
        public Block? Block { get; set; }


    }
}
