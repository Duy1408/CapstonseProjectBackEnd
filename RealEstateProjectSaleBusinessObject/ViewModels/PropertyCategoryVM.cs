using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class PropertyCategoryVM
    {
        public Guid PropertyCategoryID { get; set; }
        public string PropertyCategoryName { get; set; }
        public bool Status { get; set; }
    }
}
