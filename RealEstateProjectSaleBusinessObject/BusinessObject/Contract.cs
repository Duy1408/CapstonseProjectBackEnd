﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class Contract
    {
        public Guid ContractID { get; set; }
        public string ContractCode { get; set; }
        public string ContractType { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ExpiredTime { get; set; }//ngay het han
        public double? TotalPrice { get; set; }
        public string? Description { get; set; }
        public string? ContractDepositFile { get; set; }//file thoa thuan dat coc
        public string? ContractSaleFile { get; set; }//hop dong mua ban
        public string? PriceSheetFile { get; set; }//phieu tam tinh
        public string? ContractTransferFile { get; set; }//phieu tam tinh
        public string Status { get; set; }
        public Guid DocumentTemplateID { get; set; }
        public DocumentTemplate? DocumentTemplate { get; set; }

        public Guid BookingID { get; set; }
        public Booking? Booking { get; set; }

        public List<ContractPaymentDetail>? ContractPaymentDetails { get; set; }
        public Guid? PaymentProcessID { get; set; }
        public PaymentProcess? PaymentProcess { get; set; }
        public Guid? PromotionDetailID { get; set; }

        public PromotionDetail? PromotionDetail { get; set; }
        public Guid CustomerID { get; set; }
        public Customer? Customer { get; set; }


    }
}
