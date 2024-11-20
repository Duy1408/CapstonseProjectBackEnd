using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class UnitTypeUpdateDTO
    {
        public int? BathRoom { get; set; }
        public IFormFileCollection? Image { get; set; }
        public double? NetFloorArea { get; set; }
        public double? GrossFloorArea { get; set; }
        public int? BedRoom { get; set; }
        public int? KitchenRoom { get; set; }
        public int? LivingRoom { get; set; }
        public int? NumberFloor { get; set; }
        public int? Basement { get; set; }
        public bool? Status { get; set; }
        public Guid? PropertyTypeID { get; set; }
    }
}
