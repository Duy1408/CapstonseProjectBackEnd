using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class Project
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
        public string? Image { get; set; }
        public string Status { get; set; }
        public List<Zone> Zones { get; set; }
        public List<PaymentPolicy>? PaymentPolicies { get; set; }
        public List<Salespolicy>? Salespolicies { get; set; }
        public List<ProjectCategoryDetail>? ProjectCategoryDetails { get; set; }
        public List<UnitType>? UnitTypes { get; set; }
        public List<PanoramaImage>? PanoramaImages { get; set; }


    }
}
