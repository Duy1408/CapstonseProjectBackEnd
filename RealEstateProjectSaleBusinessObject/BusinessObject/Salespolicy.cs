using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class Salespolicy
    {
        public Guid SalesPolicyID { get; set; }
        public DateTime ExpressTime { get; set; }
        public string PeopleApplied { get; set; }
        public string SalesPolicyType { get; set; }
        public bool Status { get; set; }
        public Guid ProjectID { get; set; }
        public Project? Project { get; set; }

        public List<PaymentProcess>? PaymentProcesses { get; set; }
        public List<Promotion>? Promotions { get; set; }

    }
}
