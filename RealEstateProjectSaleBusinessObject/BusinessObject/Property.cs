using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class Property
    {
        public Guid PropertyID { get; set; }
        public string PropertyCode { get; set; }
        public string? View { get; set; }
        public double? PriceSold { get; set; }

        public string Status { get; set; }

        public List<Comment>? Comments { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<OpenForSaleDetail>? OpenForSaleDetails { get; set; }
        public Guid? UnitTypeID { get; set; }
        public UnitType? UnitType { get; set; }
        public Guid? FloorID { get; set; }
        public Floor? Floor { get; set; }
        public Guid? BlockID { get; set; }
        public Block? Block { get; set; }
        public Guid? ZoneID { get; set; }
        public Zone? Zone { get; set; }
        public Guid ProjectCategoryDetailID { get; set; }
        public ProjectCategoryDetail? ProjectCategoryDetail { get; set; }

    }
}
