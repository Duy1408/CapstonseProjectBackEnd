using Microsoft.AspNetCore.Http;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class ContractHistoryCreateDTO
    {

        public Guid ContractHistoryID { get; set; }
        public string NotarizedContractCode { get; set; }
        public string Note { get; set; }
        public IFormFile AttachFile { get; set; }
        public Guid CustomerID { get; set; }
        public Guid ContractID { get; set; }
    }
}
