using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class PaymentProcessDetail
    {
        public Guid PaymentProcessDetailID { get; set; }
        public int PaymentStage { get; set; }//dot1
        [Column(TypeName = "date")]
        public DateTime? Period { get; set; }//Thời hạn
        public float? Percentage { get; set; }
        public double? Amount { get; set; }
        public Guid PaymentProcessID { get; set; }
        public PaymentProcess? PaymentProcess { get; set; }
    }
}
