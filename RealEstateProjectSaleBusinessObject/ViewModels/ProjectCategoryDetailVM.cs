using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class ProjectCategoryDetailVM
    {
        public Guid ProjectID { get; set; }
        public string ProjectName { get; set; }
        public Guid PropertyCategoryID { get; set; }
        public string PropertyCategoryName { get; set; }
    }
}
