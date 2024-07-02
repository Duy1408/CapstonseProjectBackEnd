using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class PropertyType
    {
        public Guid PropertyTypeID { get; set; }
        public string Name { get; set; }
        public List<PromotionDetail>? PromotionDetails { get; set; }
        public List<Property>? Properties { get; set; }
    }
}
