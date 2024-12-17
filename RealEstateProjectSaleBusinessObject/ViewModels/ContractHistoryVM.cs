using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.JsonConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class ContractHistoryVM
    {
        public Guid ContractHistoryID { get; set; }
        public string NotarizedContractCode { get; set; }
        public string? Note { get; set; }
        public string AttachFile { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreatedTime { get; set; }
        public Guid CustomerID { get; set; }
        public string FullName { get; set; }
        public Guid ContractID { get; set; }
        public string ContractCode { get; set; }
    }
}
