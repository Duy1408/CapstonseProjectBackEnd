using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class OpeningForSaleUpdateDTO
    {
        public string? DecisionName { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? CheckinDate { get; set; }
        public string? SaleType { get; set; }
        public double? ReservationPrice { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
        public Guid? ProjectCategoryDetailID { get; set; }

    }
}
