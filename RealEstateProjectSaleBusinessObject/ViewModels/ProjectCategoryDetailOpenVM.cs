using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class ProjectCategoryDetailOpenVM
    {
        public Guid ProjectCategoryDetailID { get; set; }
        public Guid ProjectID { get; set; }
        public string ProjectName { get; set; }
        public Guid PropertyCategoryID { get; set; }
        public string PropertyCategoryName { get; set; }
        public bool OpenForSale { get; set; }
    }
}
