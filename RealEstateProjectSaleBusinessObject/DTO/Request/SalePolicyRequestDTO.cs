using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Request
{
    public class SalePolicyRequestDTO
    {
        public string SalesPolicyType { get; set; }
        public DateTime ExpressTime { get; set; }
        public string? PeopleApplied { get; set; }
        public bool Status { get; set; }
    }
}
