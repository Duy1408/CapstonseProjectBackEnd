using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class ContractCreateDTO
    {
        public Guid ContractID { get; set; }
        public DateTime? DateSigned { get; set; }
        public DateTime? UpdateUsAt { get; set; }
        public DateTime CreatedStAt { get; set; }
        public string ContractType { get; set; }
        public bool Status { get; set; }
        public Guid BookingID { get; set; }
    }
}
