using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class ContractHistory
    {
        public Guid ContractHistoryID { get; set; }
        public string NotarizedContractCode { get; set; }
        public string Note { get; set; }
        public string AttachFile { get; set; }
        public Guid CustomerID { get; set; }
        public Customer? Customer { get; set; }
        public Guid ContractID { get; set; }
        public Contract? Contract { get; set; }


    }
}
