using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class ContractUpdateDTO
    {
        public string? ContractCode { get; set; }
        public string? ContractType { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ExpiredTime { get; set; }
        public double? TotalPrice { get; set; }
        public string? Description { get; set; }
        public IFormFile? ContractDepositFile { get; set; }
        public IFormFile? ContractSaleFile { get; set; }
        public IFormFile? PriceSheetFile { get; set; }
        public IFormFile? ContractTransferFile { get; set; }
        public string? Status { get; set; }
        public Guid? DocumentTemplateID { get; set; }
        public Guid? BookingID { get; set; }
        public Guid? CustomerID { get; set; }
        public Guid? PaymentProcessID { get; set; }
        public Guid? PromotionDetailID { get; set; }

    }
}
