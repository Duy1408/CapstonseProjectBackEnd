using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class ProjectCreateDTO
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
        public IFormFile? Images { get; set; }
        public string Status { get; set; }

    }
}
