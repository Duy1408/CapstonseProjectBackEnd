using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class ContractVM
    {
        public Guid ContractID { get; set; }
        public string ContractCode { get; set; }
        public string ContractName { get; set; }
        public string ContractType { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DateSigned { get; set; }
        public DateTime? ExpiredTime { get; set; }
        public double? TotalPrice { get; set; }
        public string? Description { get; set; }
        public string? ContractFile { get; set; }
        public string Status { get; set; }
        public Guid? DocumentID { get; set; }
        public string DocumentName { get; set; }
        public Guid BookingID { get; set; }
        public Guid CustomerID { get; set; }
        public Guid? PaymentProcessID { get; set; }
        public string PaymentProcessName { get; set; }
    }
}
