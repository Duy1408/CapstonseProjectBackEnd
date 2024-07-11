using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class OpeningForSaleVM
    {
        public Guid OpeningForSaleID { get; set; }
        public string DescriptionName { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string? ReservationTime { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public Guid ProjectID { get; set; }
        public string ProjectName { get; set; }
    }
}
