using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class ProjectUpdateDTO
    {
        public string? ProjectName { get; set; }
        public string? CommericalName { get; set; }
        public string? ShortName { get; set; }
        public string? Address { get; set; }
        public string? Commune { get; set; }
        public string? District { get; set; }
        public double? DepositPrice { get; set; }
        public string? Summary { get; set; }
        public int? LicenseNo { get; set; }
        public DateTime? DateOfIssue { get; set; }
        public string? CampusArea { get; set; }
        public string? PlaceofIssue { get; set; }
        public string? Code { get; set; }
        public IFormFile? Image { get; set; }
        public string? Status { get; set; }
    }
}
