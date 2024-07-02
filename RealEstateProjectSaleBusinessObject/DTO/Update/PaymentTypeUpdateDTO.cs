﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class PaymentTypeUpdateDTO
    {
        [JsonIgnore]
        public Guid PaymentTypeID { get; set; }
        public string PaymentName { get; set; }
    }
}
