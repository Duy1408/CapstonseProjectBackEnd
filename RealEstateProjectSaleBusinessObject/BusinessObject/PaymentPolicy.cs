﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class PaymentPolicy
    {
        public Guid PaymentPolicyID { get; set; }
        public string PaymentPolicyName { get; set; }
        public int? LateDate { get; set; }
        public bool Status { get; set; }
        public double? PercentLate{ get; set; }
        //public Guid? ProjectID { get; set; }
        //public Project? Project { get; set; }
        public List<ContractPaymentDetail>? ContractPaymentDetails { get; set; }
        public List<Project>? Projects { get; set; }


    }
}
