﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class PropertyTypeVM
    {
        public Guid PropertyTypeID { get; set; }
        public string PropertyTypeName { get; set; }
        public Guid PropertyCategoryID { get; set; }
        public string PropertyCategoryName { get; set; }
    }
}
