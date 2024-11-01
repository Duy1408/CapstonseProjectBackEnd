using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class PropertyVM
    {
        public Guid PropertyID { get; set; }
        public string PropertyCode { get; set; }
        public string? View { get; set; }
        public double? PriceSold { get; set; }
        public string Status { get; set; }
        public Guid? UnitTypeID { get; set; }
        public int BathRoom { get; set; }
        public int BedRoom { get; set; }
        public int KitchenRoom { get; set; }
        public int LivingRoom { get; set; }
        public int? NumberFloor { get; set; }
        public int? Basement { get; set; }
        public double? NetFloorArea { get; set; }
        public double? GrossFloorArea { get; set; }
        public string? ImageUnitType { get; set; }
        public Guid? FloorID { get; set; }
        public int NumFloor { get; set; }
        public Guid? BlockID { get; set; }
        public string BlockName { get; set; }
        public Guid? ZoneID { get; set; }
        public string ZoneName { get; set; }
        public Guid ProjectCategoryDetailID { get; set; }
        public string ProjectName { get; set; }
        public string PropertyCategoryName { get; set; }

    }
}
