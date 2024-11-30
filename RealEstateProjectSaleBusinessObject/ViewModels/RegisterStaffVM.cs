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
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPass { get; set; }

        public string Name { get; set; }
        public string PersonalEmail { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }
        public IFormFile? Image { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string Nationality { get; set; }
        public string? Placeoforigin { get; set; }
        public string? PlaceOfresidence { get; set; }
    }
}
