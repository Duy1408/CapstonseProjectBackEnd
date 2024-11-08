using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class PaymentProcessDetailUpdateDTO
    {
        public int? PaymentStage { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Period { get; set; }
        public float? Percentage { get; set; }
        public double? Amount { get; set; }
        public Guid? PaymentProcessID { get; set; }

    }
}
