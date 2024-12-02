using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class RegisterStaffVM
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPass { get; set; }
        public string? Name { get; set; }
        public string? PersonalEmail { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }
        public IFormFile? Image { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string? Nationality { get; set; }
        public string? PlaceOfOrigin { get; set; }
        public string? PlaceOfResidence { get; set; }
    }
}
