﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class BlockVM
    {
        public Guid BlockID { get; set; }
        public string BlockName { get; set; }
        public string? ImageBlock { get; set; }
    }
}