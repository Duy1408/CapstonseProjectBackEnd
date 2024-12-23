﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class PropertyCategory
    {
        public Guid PropertyCategoryID { get; set; }
        public string PropertyCategoryName { get; set; }
        public bool Status { get; set; }
        public List<PropertyType>? PropertyTypes { get; set; }
        public List<ProjectCategoryDetail>? ProjectCategoryDetails { get; set; }

    }
}
