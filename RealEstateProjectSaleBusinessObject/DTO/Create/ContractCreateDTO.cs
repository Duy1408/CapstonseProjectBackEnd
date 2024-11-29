using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class ContractCreateDTO
    {
        [JsonIgnore]
        public Guid ContractID { get; set; }
        [JsonIgnore]
        public string? ContractCode { get; set; }
        [JsonIgnore]
        public string? ContractType { get; set; }
        [JsonIgnore]
        public DateTime CreatedTime { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedTime { get; set; }
        [JsonIgnore]
        [Column(TypeName = "date")]
        public DateTime? ExpiredTime { get; set; }
        public double? TotalPrice { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public IFormFile? ContractDepositFile { get; set; }
        [JsonIgnore]
        public IFormFile? ContractSaleFile { get; set; }
        [JsonIgnore]
        public IFormFile? PriceSheetFile { get; set; }
        [JsonIgnore]
        public IFormFile? ContractTransferFile { get; set; }
        [JsonIgnore]
        public string? Status { get; set; }
        [JsonIgnore]
        public Guid? DocumentTemplateID { get; set; }
        public Guid BookingID { get; set; }
        [JsonIgnore]
        public Guid CustomerID { get; set; }
        [JsonIgnore]
        public Guid? PaymentProcessID { get; set; }
        [JsonIgnore]
        public Guid? PromotionDetailID { get; set; }

    }
}
