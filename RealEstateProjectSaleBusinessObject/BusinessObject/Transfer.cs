using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class Transfer
    {
        public Guid TransferID { get; set; }
        public string Notarizedcontractcode { get; set; }
        public string Note { get; set; }
        public string AttachFile { get; set; }
        public Guid CustomerID { get; set; }
        public Customer? Customer { get; set; }
        public Guid ContractID { get; set; }
        public Contract? Contract { get; set; }


    }
}
