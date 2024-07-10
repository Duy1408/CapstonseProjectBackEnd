using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class SalepolicyVM
    {
        public Guid SalesPolicyID { get; set; }
        public string SalesPolicyType { get; set; }
        public DateTime ExpressTime { get; set; }
        public string? PeopleApplied { get; set; }
        public bool Status { get; set; }
        public string ProjectName { get; set; }

    }
}
