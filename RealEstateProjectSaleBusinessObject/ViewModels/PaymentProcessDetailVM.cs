﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class PaymentProcessDetailVM
    {
        public Guid PaymentProcessDetailID { get; set; }
        public int PaymentStage { get; set; }
        public DateTime? Period { get; set; }
        public float? Percentage { get; set; }
        public double Amount { get; set; }
        public Guid PaymentProcessID { get; set; }
        public string PaymentProcessName { get; set; }
    }
}
