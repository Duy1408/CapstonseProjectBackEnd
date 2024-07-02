﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class Promotion
    {
        public Guid PromotionID { get; set; }
        public string PromotionName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
        public Guid SalesPolicyID { get; set; }
        public Salespolicy? Salespolicy { get; set; }
        public List<PromotionDetail>? PromotionDetails { get; set; }


    }
}
