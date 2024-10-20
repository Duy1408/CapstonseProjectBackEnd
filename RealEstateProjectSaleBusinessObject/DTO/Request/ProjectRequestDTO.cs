using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Request
{
    public class ProjectRequestDTO
    {
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
        public IFormFileCollection? Images { get; set; }
    }
}
