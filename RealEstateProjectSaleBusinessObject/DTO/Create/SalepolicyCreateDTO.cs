using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class SalepolicyCreateDTO
    {
        public Guid SalesPolicyID { get; set; }
        public string SalesPolicyType { get; set; }
        [Column(TypeName = "date")]
        public DateTime ExpressTime { get; set; }
        public string? PeopleApplied { get; set; }
        public bool Status { get; set; }
        public Guid ProjectID { get; set; }

    }
}
