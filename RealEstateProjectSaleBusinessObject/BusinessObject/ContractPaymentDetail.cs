using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class ContractPaymentDetail
    {
      
        public Guid ContractPaymentDetailID { get; set; }
        public string Paymentprogress { get; set; }
        public DateTime Paymentduedate { get; set; }
        public int Customervaluepaid { get; set; }
        public string? Note { get; set; }
        public List<Payment>? Payments { get; set; }
        public Guid ContractID { get; set; }
        public Contract? Contract {  get; set; }





    }
}
