﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class PaymentTypeCreateDTO
    {
        public Guid PaymentTypeID { get; set; }
        public string PaymentName { get; set; }
    }
}
