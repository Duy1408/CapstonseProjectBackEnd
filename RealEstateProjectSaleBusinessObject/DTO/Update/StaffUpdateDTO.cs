using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class StaffUpdateDTO
    {
        public string? Name { get; set; }
        public string? PersonalEmail { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }
        public IFormFile? Image { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string? Nationality { get; set; }
        public string? Placeoforigin { get; set; }
        public string? PlaceOfresidence { get; set; }
        public bool? Status { get; set; }
        public Guid? AccountID { get; set; }

    }
}
