﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class PaymentProcessCreateDTO
    {
        [JsonIgnore]
        public Guid PaymentProcessID { get; set; }
        public string PaymentProcessName { get; set; }
        public double? Discount { get; set; }
        public double? TotalPrice { get; set; }
        public Guid SalesPolicyID { get; set; }
    }
}
