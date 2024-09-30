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
        public string PropertyTypeName { get; set; }
        public List<PromotionDetail>? PromotionDetails { get; set; }
        public Guid PropertyCategoryID { get; set; }
        public PropertyCategory? PropertyCategory { get; set; }
        public List<UnitType>? UnitTypes { get; set; }
    }
}
