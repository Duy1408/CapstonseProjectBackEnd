using RealEstateProjectSaleBusinessObject.JsonConverters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class ContractVM
    {
        public Guid ContractID { get; set; }
        public string ContractCode { get; set; }
        public string ContractName { get; set; }
        public string ContractType { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreatedTime { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? UpdatedTime { get; set; }
        [Column(TypeName = "date")]
        [JsonConverter(typeof(DateOnlyConverter))]
        public DateTime? DateSigned { get; set; }
        [Column(TypeName = "date")]
        [JsonConverter(typeof(DateOnlyConverter))]
        public DateTime? ExpiredTime { get; set; }
        public double? TotalPrice { get; set; }
        public string? Description { get; set; }
        public string? ContractDepositFile { get; set; }
        public string? ContractSaleFile { get; set; }
        public string Status { get; set; }
        public Guid? DocumentTemplateID { get; set; }
        public string DocumentName { get; set; }
        public Guid BookingID { get; set; }
        public Guid CustomerID { get; set; }
        public string FullName { get; set; }
        public Guid? PaymentProcessID { get; set; }
        public string PaymentProcessName { get; set; }
    }
}
