﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class FloorVM
    {
        public Guid FloorID { get; set; }
        public int NumFloor { get; set; }
        public string? ImageFloor { get; set; }
    }
}