using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class PromotionUpdateDTO
    {
        public string? PromotionName { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
        public Guid? SalesPolicyID { get; set; }


    }
}
