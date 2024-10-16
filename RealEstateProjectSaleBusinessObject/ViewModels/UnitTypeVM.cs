﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class UnitTypeVM
    {
        public Guid UnitTypeID { get; set; }
        public int BathRoom { get; set; }
        public List<string>? Image { get; set; }
        public double? NetFloorArea { get; set; }
        public double? GrossFloorArea { get; set; }
        public int BedRoom { get; set; }
        public int KitchenRoom { get; set; }
        public int LivingRoom { get; set; }
        public int? NumberFloor { get; set; }
        public int? Basement { get; set; }
        public bool Status { get; set; }
        public Guid? PropertyTypeID { get; set; }
        public string PropertyTypeName { get; set; }
        public Guid ProjectID { get; set; }
        public string ProjectName { get; set; }

    }
}