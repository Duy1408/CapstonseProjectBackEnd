﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class FloorUpdateDTO
    {
        public int? NumFloor { get; set; }
        public IFormFileCollection? ImageFloor { get; set; }
        public bool? Status { get; set; }
        public Guid? BlockID { get; set; }
    }
}
