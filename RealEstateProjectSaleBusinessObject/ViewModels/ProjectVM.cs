using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class ProjectVM
    {
        public Guid ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string Location { get; set; }
        public string? Investor { get; set; }
        public string? GeneralContractor { get; set; }
        public string? DesignUnit { get; set; }
        public string? TotalArea { get; set; }
        public string? Scale { get; set; }
        public string? BuildingDensity { get; set; }
        public string? TotalNumberOfApartment { get; set; }
        public string? LegalStatus { get; set; }
        public string? HandOver { get; set; }
        public string? Convenience { get; set; }
        public List<string>? Images { get; set; }
        public string Status { get; set; }


    }
}
