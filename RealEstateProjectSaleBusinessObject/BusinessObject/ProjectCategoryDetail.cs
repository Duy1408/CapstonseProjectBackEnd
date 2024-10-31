using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class ProjectCategoryDetail
    {
        public Guid ProjectCategoryDetailID { get; set; }
        public Guid ProjectID { get; set; }
        public Project? Project { get; set; }
        public Guid PropertyCategoryID { get; set; }
        public PropertyCategory? PropertyCategory { get; set; }
       public List<Property> Properties { get; set; }

    }
}
